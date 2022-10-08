using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Icity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Icity.Pages
{
    public class IndexModel : PageModel


    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IcityContext _context;
        public List<Category> Categories { get; set; }
        public List<AddListing> addListings { get; set; }
        private UserManager<ApplicationUser> UserManager { get; }
        public IndexModel(IcityContext context, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            UserManager = userManager;

        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            addListings = _context.AddListings.Include(e => e.Category).OrderByDescending(e=>e.AddListingId).Take(5).ToList();
        }
        public async Task<IActionResult> OnPostAddTFavourite([FromBody] int listingid)
        {
            bool favouriteflag = false;
            var user = await UserManager.GetUserAsync(User);
            if (user==null)
            {
                return new JsonResult(favouriteflag);
            }
            var favouriteLi = _context.Favourites.Where(a => a.UserId == user.Id && a.AddListingId == listingid).FirstOrDefault(); ;
            if (favouriteLi == null)
            {
                var favouriteobj = new Favourite() { AddListingId = listingid, UserId = user.Id };

                _context.Favourites.Add(favouriteobj);
                favouriteflag = true;
            }
            else
                _context.Favourites.Remove(favouriteLi);
            _context.SaveChanges();
            return new JsonResult(favouriteflag);
        }

    }
}

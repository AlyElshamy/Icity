using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages
{
    
    public class DashBoardListingTableModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public List<AddListing> Listings =new List<AddListing>();

        public UserManager<ApplicationUser> UserManager { get; }

        public DashBoardListingTableModel(UserManager<ApplicationUser> userManager, IcityContext Context)
        {
            UserManager = userManager;
            _context = Context;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user==null)
            {
                return Redirect("/identity/account/login");

            }
            Listings = _context.AddListings.Where(a=>a.CreatedByUser==user.Email).ToList();
            return Page();
        }
    }
}

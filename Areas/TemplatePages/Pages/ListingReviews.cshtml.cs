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
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages
{
    public class ListingReviewsModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public UserManager<ApplicationUser> UserManager { get; }
        public List<Review> reviewlist = new List<Review>();

        public ListingReviewsModel(UserManager<ApplicationUser> userManager, IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            UserManager = userManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        public async Task<IActionResult> OnGet()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/identity/login");
            }
            try
            {
                var listingid = _context.AddListings.Where(a => a.CreatedByUser == user.Email);
                foreach (var item in listingid)
                {
                    var rev = _context.Reviews.Where(a => a.AddListingId == item.AddListingId);
                    reviewlist.AddRange(rev);
                }
                return Page();

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("somthing went Error");
            }
            return Page();


        }
    }
}

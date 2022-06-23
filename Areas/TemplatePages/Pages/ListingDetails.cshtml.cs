using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages
{
    public class ListingDetailsModel : PageModel
    {
        private IcityContext _context;


        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> userManager;
        [ViewData]
        public AddListing listing { get; set; }
        public static int listid { get; set; }
        public ListingDetailsModel(IcityContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _toastNotification = toastNotification;
            this.userManager = userManager;
        }
        [BindProperty]
        public Review Review { get; set; }
       
        public ApplicationUser curruntuser { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {

                listid = id;
                 var Listing = await _context.AddListings.Include(e=>e.Category).Include(a=>a.ListingPhotos).Include(a=>a.ListingVideos).FirstOrDefaultAsync(m => m.AddListingId == id);
                curruntuser = await userManager.FindByEmailAsync(Listing.CreatedByUser);

                ViewData["listings"] = Listing;
                if (Listing == null)
                {
                    return Redirect("../Error");
                }
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {
                Review.AddListingId = listid;
                Review.ReviewDate = DateTime.Now;
                _context.Reviews.Add(Review);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Thanks For Your Review..");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }
            return RedirectToPage("ListingDetails",new {id= listid});
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages.ClassifiedAds
{
    public class DeleteModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public DeleteModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        [BindProperty]
        public Icity.Models.ClassifiedAds classifiedAds { get; set; }
        public static int listobjid { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                classifiedAds = await _context.ClassifiedAds.Include(a => a.ClassifiedAsdMedias).FirstOrDefaultAsync(m => m.ClassifiedAdsID == id);
                if (classifiedAds == null)
                {
                    return RedirectToPage("/PageNF");
                }

                return Page();

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
                _context.ClassifiedAds.Remove(classifiedAds);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Listing Deleted successfully");
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
            }



            return RedirectToPage("./Index");
        }
    }
}


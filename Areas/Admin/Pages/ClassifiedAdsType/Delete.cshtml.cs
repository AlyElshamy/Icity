using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System;
using System.IO;

namespace Icity.Areas.Admin.Pages.ClassifiedAdsType
{
    public class DeleteModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public Icity.Models.ClassifiedAdsType ClassifiedAdsType { get; set; }
        public DeleteModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                ClassifiedAdsType = await _context.ClassifiedAdsTypes.FirstOrDefaultAsync(m => m.ClassifiedAdsTypeID == id);

                if (ClassifiedAdsType == null)
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



        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                ClassifiedAdsType = await _context.ClassifiedAdsTypes.FindAsync(id);
                if (ClassifiedAdsType != null)
                {

                    _context.ClassifiedAdsTypes.Remove(ClassifiedAdsType);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Classified Type Ads Deleted successfully");
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/ClassifiedAdsMedia/Media/" + ClassifiedAdsType.ClassifiedAdsTypeID);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }
                else
                    return Redirect("../Error");
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");

                return Page();

            }

            return RedirectToPage("./Index");
        }

    }

}

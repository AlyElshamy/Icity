using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.Admin.Pages.SubCategories
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
        //[BindProperty]
        public SubCategory Subcategory { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                 Subcategory = await _context.SubCategories.FirstOrDefaultAsync(m => m.SubCategoryID == id);

                if (Subcategory == null)
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
            int catId = 0;
            try
            {
               var Subcategory =_context.SubCategories.Where(a=>a.SubCategoryID==id).FirstOrDefault();
                
                if (Subcategory != null)
                {
                    catId = Subcategory.CategoryId;
                    _context.SubCategories.Remove(Subcategory);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Sub Category Deleted successfully");
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/SubCategory/" + Subcategory.SubCategoryPic);
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

            return RedirectToPage("./Index", new { id = catId });

        }
    }
}

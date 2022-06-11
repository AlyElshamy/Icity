using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;
using Icity.Data;
using Icity.Models;

namespace Icity.Areas.Admin.Pages.Categories
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
        public Category category { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

                if (category == null)
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
                category = await _context.Categories.FindAsync(id);
                if (category != null)
                {

                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Category Deleted successfully");
                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/Category/" + category.CategoryPic);
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

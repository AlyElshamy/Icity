using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Icity.Data;
using Icity.Models;

namespace Icity.Areas.Admin.Pages.Categories
{
    public class EditModel : PageModel
    {

        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public EditModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                var model = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
                if (model == null)
                {
                    return Page();
                }
                var uniqeFileName = "";

                if (Response.HttpContext.Request.Form.Files.Count() > 0)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Category");
                    string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);
                    uniqeFileName = Guid.NewGuid().ToString("N") + ext;
                    string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);
                    using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                    }
                    model.CategoryPic = "Images/Category/" + uniqeFileName;
                }
                model.Description = category.Description;
                model.CategoryTitleAr = category.CategoryTitleAr;
                model.CategoryTitleEn = category.CategoryTitleEn;

                _context.Attach(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("category Edited successfully");

            }
            catch (Exception)
            {
               
                    _toastNotification.AddErrorToastMessage("Something went wrong");
                
            }

            return RedirectToPage("./Index");
        }

       
    }
}

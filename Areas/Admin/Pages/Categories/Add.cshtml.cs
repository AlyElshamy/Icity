using System;
using System.IO;
using System.Linq;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace Icity.Areas.Admin.Pages.Categories
{
    public class AddModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public AddModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }

        public void OnGet()
        {
          
        }
        public IActionResult OnPost(Category model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var uniqeFileName = "";

                if (Response.HttpContext.Request.Form.Files.Count() > 0)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Category");
                   
                     string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);

                    uniqeFileName = Guid.NewGuid().ToString("N") +ext;

                    string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);

                    using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                    }
                    model.CategoryPic = "Images/Category/" + uniqeFileName;
                }
            

                _context.Categories.Add(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Plan Added successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
          
            return Redirect("./Index");

        }
    }
}

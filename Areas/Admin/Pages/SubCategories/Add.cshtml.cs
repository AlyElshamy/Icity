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
using NToastNotify;

namespace Icity.Areas.Admin.Pages.SubCategories
{
    public class AddModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public int CategoryIdProp { get; set; }
        public AddModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
        }

        public void OnGet(int categoryid)
        {
            CategoryIdProp = categoryid;
        }
        public IActionResult OnPost(SubCategory model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index", new { id = model.CategoryId });

            }
            try
            {
                var uniqeFileName = "";
                if (Response.HttpContext.Request.Form.Files.Count() > 0)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/SubCategory");

                    string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);

                    uniqeFileName = Guid.NewGuid().ToString("N") + ext;

                    string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);

                    using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                    {
                        Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                    }
                    model.SubCategoryPic = "Images/SubCategory/" + uniqeFileName;
                }
                _context.SubCategories.Add(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("SubCategory Added successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return RedirectToPage("./Index",new {id=model.CategoryId });

        }
    }
}

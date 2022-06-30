using System;
using System.IO;
using System.Linq;
using Icity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
namespace Icity.Areas.Admin.Pages.ClassifiedAdsType
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
            public IActionResult OnPost(Icity.Models.ClassifiedAdsType model)
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
                        string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/ClassifiedAdsMedia/Media/");

                        string ext = Path.GetExtension(Response.HttpContext.Request.Form.Files[0].FileName);

                        uniqeFileName = Guid.NewGuid().ToString("N") + ext;

                        string uploadedImagePath = Path.Combine(uploadFolder, uniqeFileName);

                        using (FileStream fileStream = new FileStream(uploadedImagePath, FileMode.Create))
                        {
                            Response.HttpContext.Request.Form.Files[0].CopyTo(fileStream);
                        }
                        model.TypePic = "Images/ClassifiedAdsMedia/Media/" + uniqeFileName;
                    }


                    _context.ClassifiedAdsTypes.Add(model);
                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Classified Ads Type Added successfully");

                }
                catch (Exception)
                {

                    _toastNotification.AddErrorToastMessage("Something went wrong");
                }

                return Redirect("./Index");

            }
        
    }
}

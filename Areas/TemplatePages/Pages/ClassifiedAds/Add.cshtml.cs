using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;


namespace Icity.Areas.TemplatePages.Pages.ClassifiedAds
{
    public class AddModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public static List<Branch> Branches;

        [BindProperty]
        public Icity.Models.ClassifiedAds classifiedAds { get; set; }
        private UserManager<ApplicationUser> _userManager { get; }
        private static ApplicationUser user { get; set; }

        public AddModel(UserManager<ApplicationUser> userManager, IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
        public async Task<IActionResult> OnGet()
        {
            user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/identity/account/login");
            }
            return Page();
        }

        public IActionResult OnPost(IFormFile MainPhoto, IFormFileCollection Medias, Icity.Models.ClassifiedAds classifiedAds)
        {
            if (classifiedAds.ClassifiedAdsTypeID == 0)
            {
                _toastNotification.AddErrorToastMessage("Select Classifed Ads Type");
                return Page();
            }
            if (classifiedAds.ProductStatusID == 0)
            {
                _toastNotification.AddErrorToastMessage("Select Ads Status ");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (MainPhoto != null)
            {
                string folder = "Images/ClassifiedAdsMedia/Media/";
                classifiedAds.MainPhoto = UploadImage(folder, MainPhoto);
            }
            try
            {

                List<ClassifiedAsdMedia> classifiedAsdMedias = new List<ClassifiedAsdMedia>();
                if (Medias.Count() > 0)
                {
                    for (int i = 0; i < Medias.Count(); i++)
                    {
                        ClassifiedAsdMedia classifiedAsdMediaObj = new ClassifiedAsdMedia();
                        if (Medias[i] != null)
                        {
                            string folder = "Images/ClassifiedAdsMedia/Media/";
                            classifiedAsdMediaObj.MediaUrl = UploadImage(folder, Medias[i]);
                            classifiedAsdMediaObj.MediaDate = DateTime.Now;
                        }

                        classifiedAsdMedias.Add(classifiedAsdMediaObj);
                    }
                    classifiedAds.ClassifiedAsdMedias = classifiedAsdMedias;
                }

                classifiedAds.Status = false;
                classifiedAds.AddedBy = user.Email;
                classifiedAds.AddedDate = DateTime.Now;
                classifiedAds.PayedDate = null;
                _context.ClassifiedAds.Add(classifiedAds);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Classified Ads  Added successfully");
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
                return Page();
            }
            return Page();


        }

    }
}



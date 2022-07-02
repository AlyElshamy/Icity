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
        public Icity.Models.ClassifiedAds classifiedAds { get; set; }
        public static int listobjid { get; set; }
      
        public IActionResult OnPostDeletePhotoById([FromBody] int PhotoId)
        {
            try
            {
                var classifiedMedia = _context.ClassifiedAsdMedias.Where(a => a.MediaId == PhotoId).FirstOrDefault();
                if (classifiedMedia == null)
                {
                    return Page();
                }
                _context.ClassifiedAsdMedias.Remove(classifiedMedia);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Somthing Went Error..");
                return Page();
            }
            return new JsonResult(PhotoId);

        }
      

        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                classifiedAds = await _context.ClassifiedAds.Include(a => a.ClassifiedAsdMedias).FirstOrDefaultAsync(m => m.ClassifiedAdsID == id);
                if (classifiedAds == null)
                {
                    return Redirect("../Error");
                }
                
                return Page();

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Page();
        }
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
        public IActionResult OnPost(IFormFile MainPhoto, IFormFileCollection Medias)
        {
            var model = _context.ClassifiedAds.Where(c => c.ClassifiedAdsID == classifiedAds.ClassifiedAdsID).FirstOrDefault();
            if (model == null)
            {
                return Page();
            }
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
                model.MainPhoto = UploadImage(folder, MainPhoto);
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
                    model.ClassifiedAsdMedias = classifiedAsdMedias;
                }

                model.AddedDate = classifiedAds.AddedDate;
                model.PayedDate = classifiedAds.PayedDate;
                model.Title = classifiedAds.Title;
                model.Price = classifiedAds.Price;
                model.ClassifiedAdsLocation = classifiedAds.ClassifiedAdsLocation;
                model.AddedBy = classifiedAds.AddedBy;
                model.ClassifiedAdsTypeID = classifiedAds.ClassifiedAdsTypeID;
                model.ProductStatusID = classifiedAds.ProductStatusID;
                model.PayedDate = classifiedAds.PayedDate;
                model.Details = classifiedAds.Details;
                model.Status = classifiedAds.Status;
              

                _context.Attach(model).State = EntityState.Modified;
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Classified Ads successfully"); _context.SaveChanges();
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}


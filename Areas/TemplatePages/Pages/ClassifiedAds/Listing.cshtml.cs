using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Icity.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages.ClassifiedAds
{
    public class ListingModel : PageModel
    {

        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public ListingModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
        
        public  List<Icity.Models.ClassifiedAds> ClassifiedAdsList { get; set; }
        public static List<Icity.Models.ClassifiedAds> ClassifiedAdsListStatic { get; set; }
        [BindProperty]
        public ClassifiedAdsFilterModel FilterModel{ get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            try
            {
                ClassifiedAdsList = await _context.ClassifiedAds.Where(a=>a.Status==false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            if (ClassifiedAdsListStatic != null)
            {
                if (ClassifiedAdsListStatic.Count != 0)
                {
                    ClassifiedAdsList = ClassifiedAdsListStatic;
                }
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostSortList([FromBody] List<string> SelectedValue)
        {
            ClassifiedAdsListStatic = new List<Models.ClassifiedAds>();
            int restult = 0;
            bool checkValue = int.TryParse(SelectedValue[0], out restult);
            if (checkValue)
            {
                if (restult == 1)
                {
                    ClassifiedAdsListStatic = await _context.ClassifiedAds.Where(a => a.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).OrderBy(e => e.Price).ToListAsync();

                }
                if (restult == 2)
                {
                    ClassifiedAdsListStatic = await _context.ClassifiedAds.Where(a => a.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).OrderByDescending(e => e.Price).ToListAsync();

                }
            }

            return new JsonResult(SelectedValue);
        }
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                ClassifiedAdsList = await _context.ClassifiedAds.Where(a => a.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                if (FilterModel.CatId != 0)
                {
                    ClassifiedAdsList= await _context.ClassifiedAds.Where(e=>e.ClassifiedAdsTypeID==FilterModel.CatId && e.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                }
                if (FilterModel.Target != null)
                {
                    ClassifiedAdsList = await _context.ClassifiedAds.Where(e => e.Title.ToUpper().Contains(FilterModel.Target.ToUpper())&& e.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                }
                if (FilterModel.Location != null)
                {
                    ClassifiedAdsList = await _context.ClassifiedAds.Where(e => e.ClassifiedAdsLocation.ToUpper().Contains(FilterModel.Location.ToUpper()) && e.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                }
                if (FilterModel.Location == null&& FilterModel.Target == null&& FilterModel.CatId == 0)
                {
                    ClassifiedAdsList = new List<Models.ClassifiedAds>();
                }

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Page();
        }

    }
}


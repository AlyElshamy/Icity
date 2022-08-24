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
        public List<int> Pagenumbers = new List<int>();
        public static bool first = true;
        public static bool ajax = true;
        public ListingModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }

        public List<Icity.Models.ClassifiedAds> ClassifiedAdsList { get; set; }
        public static List<Icity.Models.ClassifiedAds> ClassifiedAdsListStatic { get; set; }
        public static List<Models.ClassifiedAds> classifiedaddsloc = new List<Models.ClassifiedAds>();
        public static List<Models.ClassifiedAds> Listings2 = new List<Models.ClassifiedAds>();

        public int pages = 2;
        [BindProperty]
        public ClassifiedAdsFilterModel FilterModel { get; set; }
        public async Task<IActionResult> OnPostBranchesList(int x)
        {
            return new JsonResult(classifiedaddsloc);
        }
        public async Task<IActionResult> OnPostPagesList([FromBody] int num)
        {

            //var alllistings = _context.AddListings.Include(a => a.Category).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToList();
            ajax = false;
            var start = (num - 1) * pages;
            ClassifiedAdsList = classifiedaddsloc.Skip(start).Take(pages).ToList();
            Listings2 = ClassifiedAdsList;
            return new JsonResult(num);
        }
        public List<int> getpagescount(List<Models.ClassifiedAds> addListings)
        {

            float number = (float)addListings.Count() / pages;
            var pagenumber = Math.Ceiling(number);
            for (int i = 1; i <= pagenumber; i++)
            {
                Pagenumbers.Add(i);
            }
            return Pagenumbers;
        }
        public async Task<IActionResult> OnGetAsync()
        {
           

            try
            {
                var alllist = await _context.ClassifiedAds.Where(a => a.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                Pagenumbers = getpagescount(alllist);
                if (first)
                {

                    classifiedaddsloc = alllist;
                    ClassifiedAdsList = alllist.Take(pages).ToList();
                    var ListCities = _context.AddListings.Select(a => a.City).Distinct();
                    first = false;
                }
                else
                    ClassifiedAdsList = Listings2;
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
                    classifiedaddsloc = ClassifiedAdsList;

                }
                if (restult == 2)
                {
                    ClassifiedAdsListStatic = await _context.ClassifiedAds.Where(a => a.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).OrderByDescending(e => e.Price).ToListAsync();
                    classifiedaddsloc = ClassifiedAdsList;

                }
            }

            return new JsonResult(SelectedValue);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ajax == false)
            {
                ClassifiedAdsList = Listings2;
                Pagenumbers = getpagescount(classifiedaddsloc);
                ajax = true;
                return Page();
            }
            try
            {
                ClassifiedAdsList = await _context.ClassifiedAds.Where(a => a.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                if (FilterModel.CatId != 0)
                {
                    ClassifiedAdsList = await _context.ClassifiedAds.Where(e => e.ClassifiedAdsTypeID == FilterModel.CatId && e.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                    classifiedaddsloc = ClassifiedAdsList;
                }
                if (FilterModel.Target != null)
                {
                    ClassifiedAdsList = await _context.ClassifiedAds.Where(e => e.Title.ToUpper().Contains(FilterModel.Target.ToUpper()) && e.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                    classifiedaddsloc = ClassifiedAdsList;
                }
                if (FilterModel.Location != null)
                {
                    ClassifiedAdsList = await _context.ClassifiedAds.Where(e => e.ClassifiedAdsLocation.ToUpper().Contains(FilterModel.Location.ToUpper()) && e.Status == false).Include(a => a.ClassifiedAsdMedias).Include(a => a.ProductStatus).Include(a => a.ClassifiedAdsType).ToListAsync();
                    classifiedaddsloc = ClassifiedAdsList;
                }
                if (FilterModel.Location == null && FilterModel.Target == null && FilterModel.CatId == 0)
                {
                    ClassifiedAdsList = new List<Models.ClassifiedAds>();
                }
                Pagenumbers = getpagescount(ClassifiedAdsList);
                ClassifiedAdsList = ClassifiedAdsList.Take(pages).ToList();
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Page();
        }

    }
}


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

namespace Icity.Areas.TemplatePages.Pages
{
    public class DisplayListingModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public DisplayListingModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }

        public List<Icity.Models.AddListing> AddListingList { get; set; }
        public static List<Icity.Models.AddListing> AddListingListStatic { get; set; }
        [BindProperty]
        public ClassifiedAdsFilterModel FilterModel { get; set; }
        public List<string> Cities = new List<string>();
        public static List<AddListing> AddListingsloc = new List<AddListing>();
        public async Task<IActionResult> OnGetAsync()
        {

            try
            {
                AddListingList = await _context.AddListings.Include(a => a.Category).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToListAsync();
                var ListCities = _context.AddListings.Select(a => a.City).Distinct();
                //AddListingsloc = AddListingList;

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            if (AddListingListStatic != null)
            {
                if (AddListingListStatic.Count != 0)
                {
                    AddListingList = AddListingListStatic;
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPostBranchesList(int x)
        {
            return new JsonResult(AddListingsloc);
        }
        public async Task<IActionResult> OnPostSortList([FromBody] List<string> SelectedValue)
        {
            AddListingListStatic = new List<Models.AddListing>();
            int restult = 0;
            bool checkValue = int.TryParse(SelectedValue[0], out restult);
            if (checkValue)
            {
                if (restult == 1)
                {
                    AddListingListStatic = await _context.AddListings.Include(a => a.Category).Include(a => a.ListingPhotos).Include(a => a.Reviews).OrderBy(e => e.Rating).ToListAsync();
                    AddListingsloc = AddListingListStatic;
                }
                if (restult == 2)
                {

                    AddListingListStatic = await _context.AddListings.Include(a => a.Category).Include(a => a.ListingPhotos).Include(a => a.Reviews).OrderByDescending(e => e.Rating).ToListAsync();
                    AddListingsloc = AddListingListStatic;
                }
            }

            return new JsonResult(SelectedValue);
        }
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                AddListingList = await _context.AddListings.Include(a => a.Category).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToListAsync();
                if (FilterModel.CatId != 0)
                {
                    AddListingList = await _context.AddListings.Include(a => a.Category).Where(e => e.CategoryId == FilterModel.CatId).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToListAsync();
                    AddListingsloc = AddListingList;
                }
                if (FilterModel.Target != null)
                {
                    AddListingList = await _context.AddListings.Include(a => a.Category).Where(e => e.Title.ToUpper().Contains(FilterModel.Target.ToUpper())).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToListAsync();
                    AddListingsloc = AddListingList;

                }
                if (FilterModel.Location != null)
                {
                    AddListingList = await _context.AddListings.Include(a => a.Category).Where(e => e.MainLocataion.ToUpper().Contains(FilterModel.Location.ToUpper())).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToListAsync();
                    AddListingsloc = AddListingList;

                }
                if (FilterModel.City != null)
                {
                    AddListingList = await _context.AddListings.Include(a => a.Category).Where(e => e.City.ToUpper().Contains(FilterModel.City.ToUpper())).Include(a => a.ListingPhotos).Include(a => a.Reviews).ToListAsync();
                    AddListingsloc = AddListingList;

                }
                if (FilterModel.Location == null && FilterModel.Target == null && FilterModel.CatId == 0 && FilterModel.City == null)
                {
                    AddListingList = new List<Models.AddListing>();
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


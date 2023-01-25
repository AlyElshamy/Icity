using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Icity.Data;

namespace Icity.Areas.Admin.Pages
{
   
    public class IndexModel : PageModel
    {
        private readonly IcityContext _context;
        public int AddListingCount { get; set; }
        public int ClassifiedAdsCount { get; set; }
        public int PaidClassifiedAdsCount { get; set; }
        public int UsersCount { get; set; }
        public string url { get; set; }
        public ApplicationDbContext _applicationDbContext { get; set; }
        public IndexModel(IcityContext context,ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _applicationDbContext = applicationDbContext;
        }
        public void OnGet()
        {
            try
            {
                url = $"{this.Request.Scheme}://{this.Request.Host}";
                AddListingCount = _context.AddListings.Count();
                ClassifiedAdsCount = _context.ClassifiedAds.Count();
                PaidClassifiedAdsCount = _context.ClassifiedAds.Where(e=>e.Status==true).Count();
                UsersCount = _applicationDbContext.Users.Count();
            }
            catch (Exception )
            {

            }
        }
    }
}

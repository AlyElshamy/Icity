using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages.ClassifiedAds
{
    public class IndexModel : PageModel
    {
        private IcityContext _context;
        public List<Icity.Models.ClassifiedAds> ClassifiedAds = new List<Icity.Models.ClassifiedAds>();

        private UserManager<ApplicationUser> _userManager { get; }
        public static List<Models.ClassifiedAds> Listings2 = new List<Models.ClassifiedAds>();

        public List<int> Pagenumbers = new List<int>();
        public static bool first = true;
        public IndexModel(UserManager<ApplicationUser> userManager, IcityContext Context)
        {
            _userManager = userManager;
            _context = Context;
        }
        public async Task<IActionResult> OnPostBranchesList([FromBody] int num)
        {
            var user = await _userManager.GetUserAsync(User);
            var alllistings = _context.ClassifiedAds.Include(a => a.ProductStatus).Where(a => a.AddedBy == user.Email).ToList();

            var start = (num - 1) * 2;
            var end = (num) * 2;
            Listings2 = alllistings.Skip(start).Take(2).ToList();
            ClassifiedAds = Listings2;
            return new JsonResult(ClassifiedAds);
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Redirect("/identity/account/login");

            }
            if (first)
            {
                ClassifiedAds = _context.ClassifiedAds.Include(a => a.ProductStatus).Where(a => a.AddedBy == user.Email).Take(2).ToList();
                Listings2 = ClassifiedAds;
                first = false;
            }
            else
                ClassifiedAds = Listings2;
            var alllistings = _context.ClassifiedAds.Include(a=>a.ProductStatus).Where(a => a.AddedBy == user.Email).ToList();

            float number = (float)alllistings.Count() / 2;
            var pagenumber = Math.Ceiling(number);
            for (int i = 1; i <= pagenumber; i++)
            {
                Pagenumbers.Add(i);
            }
            return Page();
        }
    }
}


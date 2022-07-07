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

        public IndexModel(UserManager<ApplicationUser> userManager, IcityContext Context)
        {
            _userManager = userManager;
            _context = Context;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/identity/account/login");

            }
            ClassifiedAds = _context.ClassifiedAds.Where(a => a.AddedBy == user.Email).Include(e=>e.ProductStatus).ToList();
            return Page();
        }
    }
}


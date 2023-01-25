using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Icity.Data;
using Icity.Models;
using Microsoft.EntityFrameworkCore;

namespace Icity.Areas.Admin.Pages.Countries
{
    public class IndexModel : PageModel
    {
        private IcityContext _context;
        public IndexModel(IcityContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public List<Country> countryList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            countryList =await  _context.Countries.ToListAsync();
            return Page();
        }
    }
}
    
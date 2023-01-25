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
using NToastNotify;

namespace Icity.Areas.Admin.Pages.Cities
{
    public class IndexModel : PageModel
    {
        private IcityContext _context;
        private readonly IToastNotification _toastNotification;

        public IndexModel(IcityContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }

        [BindProperty]
        public List<City> cityList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                cityList = await _context.Cities.ToListAsync();
               
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");

            }
            return Page();

        }
    }
}
    
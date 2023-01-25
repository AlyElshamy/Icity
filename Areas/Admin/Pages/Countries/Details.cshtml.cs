using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Icity.Data;
using Icity.Models;

namespace Icity.Areas.Admin.Pages.Countries
{
    public class DetailsModel : PageModel
    {

        private IcityContext _context;
        private readonly IToastNotification _toastNotification;

        public DetailsModel(IcityContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }
        [BindProperty]
        public Country country { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            try
            {
                country = await _context.Countries.FirstOrDefaultAsync(m => m.CountryId == id);
                
                if (country == null)
                {
                    return Redirect("../Error");
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

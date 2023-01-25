using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Icity.Data;
using Icity.Models;

namespace Icity.Areas.Admin.Pages.Countries
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
        public async Task<IActionResult> OnPostAsync(int ?id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                var model = _context.Countries.Where(c => c.CountryId == id.Value).FirstOrDefault();
                if (model == null)
                {
                    return Page();
                }
                
                model.CountryTlAr = country.CountryTlAr;
                model.CountryTlEn = country.CountryTlEn;
                _context.Attach(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Country Edited successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }

            return RedirectToPage("./Index");
        }

        
    }
}

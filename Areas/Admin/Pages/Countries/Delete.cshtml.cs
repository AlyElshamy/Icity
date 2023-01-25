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
using Microsoft.Extensions.Localization;
using NToastNotify;
using Icity.Data;
using Icity.Models;

namespace Icity.Areas.Admin.Pages.Countries
{
    public class DeleteModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public Country country { get; set; }
        public DeleteModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }
       
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
        public async Task<IActionResult> OnPostAsync(int id)
        {
          

            try
            {
                country = await _context.Countries.FirstOrDefaultAsync(m => m.CountryId == id);
                if (_context.Cities.Any(c => c.CountryId == id))
                                   {
                    var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    var BrowserCulture = locale.RequestCulture.UICulture.ToString();
                    if (BrowserCulture == "en-US")

                        _toastNotification.AddErrorToastMessage("You cannot delete this Country");

                    else
                        _toastNotification.AddErrorToastMessage("لا يمكنك مسح هذه البلد");

                    
                    return Page();
                }
                country = await _context.Countries.FindAsync(id);
                if (country != null)
                {
                    _context.Countries.Remove(country);
                    await _context.SaveChangesAsync();
                    
                    _toastNotification.AddSuccessToastMessage("country Deleted successfully");

                }
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

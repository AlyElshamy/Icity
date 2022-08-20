using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.Admin.Pages.Countries
{
    public class DeleteModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public DeleteModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
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



        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                country = await _context.Countries.FindAsync(id);
                if (country != null)
                {

                    _context.Countries.Remove(country);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Country Deleted successfully");
                    
                }
                else
                    return Redirect("../Error");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace Icity.Areas.Admin.Pages.Countries
{
    public class CreateModel : PageModel
    {
        private IcityContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public CreateModel(IcityContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }

        public void OnGet()
        {

        }
        public IActionResult OnPost(Country model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Try Again..");
                return Page();
            }
            try
            {
               


                _context.Countries.Add(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Country Added successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("./Index");

        }
    }
}

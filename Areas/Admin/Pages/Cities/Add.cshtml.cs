using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Icity.Data;

namespace Icity.Areas.Admin.Pages.Cities
{
    public class AddModel : PageModel
    {
        private IcityContext _context;
        private readonly IToastNotification _toastNotification;

        public AddModel(IcityContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;

        }
        
        public void OnGet()
        {
        }
        public IActionResult OnPost(Icity.Models.City model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _context.Cities.Add(model);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("City Added successfully");
            }
            catch (Exception)
            {


                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }

            return Redirect("./Index");

        }
    }
}

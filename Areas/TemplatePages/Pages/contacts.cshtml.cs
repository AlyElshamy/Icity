using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.TemplatePages.Pages
{
    public class contactsModel : PageModel
    {
        private IcityContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> userManager;
        //public ApplicationUser curruntuser { get; set; }
        [BindProperty]
        public Contact contact { get; set; }
        public contactsModel(IcityContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _toastNotification = toastNotification;
            this.userManager = userManager;
        }
        public  void OnGet()
        {
             

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Please! Fill With Valid Data And Try Again :");
                return Page();
            }
            try
            {
               var curruntuser = await userManager.GetUserAsync(User);
                contact.Email = curruntuser.Email;
                contact.SendingDate = DateTime.Now;
                //contact.FullName = curruntuser.FullName;
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Your Message Added Successfully");

            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error ");
                return Page();
            }
           
            return Page();
        }
    }
}

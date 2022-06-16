using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Icity.Areas.Admin.Pages.SubCategories
{
    public class DetailsModel : PageModel
    {
        private IcityContext _context;
        [BindProperty]
        public int categoryprop { get; set; }

        private readonly IToastNotification _toastNotification;
        public DetailsModel(IcityContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        [BindProperty]
        public SubCategory Subcategory { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                Subcategory = await _context.SubCategories.FirstOrDefaultAsync(m => m.SubCategoryID == id);

                if (Subcategory == null)
                {
                    return Redirect("../Error");
                }
                categoryprop = id;


            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }



            return Page();
        }

    }
}

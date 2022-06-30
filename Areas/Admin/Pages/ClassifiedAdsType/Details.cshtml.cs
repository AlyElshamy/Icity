using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Icity.Data;
using Icity.Models;

namespace Icity.Areas.Admin.Pages.ClassifiedAdsType
{
    public class DetailsModel : PageModel
    {
        private IcityContext _context;
        public Icity.Models.ClassifiedAdsType ClassifiedAdsTypeObj { get; set; }

        private readonly IToastNotification _toastNotification;
        public DetailsModel(IcityContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            try
            {
                ClassifiedAdsTypeObj = await _context.ClassifiedAdsTypes.FirstOrDefaultAsync(m => m.ClassifiedAdsTypeID == id);

                if (ClassifiedAdsTypeObj == null)
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


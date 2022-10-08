using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Icity.Data;
using Icity.Models;
using System.Collections.Generic;
using System.Linq;
namespace Icity.Areas.Admin.Pages
{
    public class FeedBackDetailsModel : PageModel
    {
        private readonly IcityContext _context;
        public Contact userContactMessage;
        public FeedBackDetailsModel(IcityContext context)
        {
            _context = context;

        }
        public void OnGet(int id)
        {
            userContactMessage = _context.Contacts.FirstOrDefault(e => e.ContactId == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Icity.Data;
using Icity.Models;
using System.Collections.Generic;
using System.Linq;

namespace Icity.Areas.Admin.Pages
{
    public class UsersFeedBacksModel : PageModel
    {
        private readonly IcityContext _context;
        public List<Contact> UserMsgsList;
        public UsersFeedBacksModel(IcityContext context)
        {
            _context = context;
           
        }
        public void OnGet()
        {
            UserMsgsList = _context.Contacts.ToList();
        }
    }
}

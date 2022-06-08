using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Icity.Data;

namespace Icity.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IcityContext _context;

        public IndexModel(IcityContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}

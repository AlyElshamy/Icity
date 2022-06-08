using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Icity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Icity.Pages
{
    public class IndexModel : PageModel


    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IcityContext _context;
          
        public IndexModel(IcityContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;

        }
        public void OnGet()
        {

        }

  
    }
}

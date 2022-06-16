using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Icity.Areas.Admin.Pages.SubCategories
{
    public class IndexModel : PageModel
    {
        public int categoryprop { get; set; }
        public void OnGet(int id)
        {
            categoryprop = id;
        }
    }
}

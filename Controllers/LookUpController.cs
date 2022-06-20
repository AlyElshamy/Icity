using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Icity.Data;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

namespace Icity.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LookUpController : Controller
    {
        private IcityContext _context;
        UserManager<ApplicationUser> UserManger;


        public LookUpController(IcityContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            UserManger = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> CategoriesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Categories
                         select new
                         {
                             Value = i.CategoryId,
                             Text = i.CategoryTitleEn
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
    }
}

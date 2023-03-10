using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Icity.Models;
using Microsoft.EntityFrameworkCore;

namespace Icity.Areas.Admin.Pages.SystemConfigration
{
   

    public class EditPageContentModel : PageModel
    {

        private IcityContext _context;


        public EditPageContentModel(IcityContext context)
        {
            _context = context;

        }

        [BindProperty]
        public string ContentAr { get; set; }
        [BindProperty]
        public string ContentEn { get; set; }
        [BindProperty]
        public PageContent pageContent { get; set; }
        public IActionResult OnGet(int id)
        {
            pageContent = _context.PageContents.FirstOrDefault(p => p.PageContentId == id);
            if(pageContent != null)
            {
                ContentAr = pageContent.PageTitleAr;
                ContentEn = pageContent.ContentEn;
                return Page();
            }
           
                return RedirectToPage("./PagesContent");

        }
    public  IActionResult OnPost(int id)
        {
            var model= _context.PageContents.FirstOrDefault(p => p.PageContentId == id);
            if (model!=null)
            {
                model.ContentAr = pageContent.ContentAr;
                model.ContentEn = pageContent.ContentEn;
                _context.Attach(model).State = EntityState.Modified;
                _context.SaveChanges();
            }
            
            return RedirectToPage("./PagesContent");
        }
    }
}

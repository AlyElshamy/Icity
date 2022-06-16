using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Icity.Areas.Admin.Pages.SubCategories
{
    public class SubCategoryListModel : PageModel
    {
        public int CategoryIdProp { get; set; }
        public void OnGet(int CategoryId)
        {
            CategoryIdProp = CategoryId;
        }
    }
}

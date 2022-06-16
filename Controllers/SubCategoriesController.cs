using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;

namespace Icity.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SubCategoriesController : Controller
    {
        private IcityContext _context;

        public SubCategoriesController(IcityContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int CategoryId,DataSourceLoadOptions loadOptions) {
            var subcategories = _context.SubCategories.Where(e=>e.CategoryId==CategoryId).Select(i => new {
                i.SubCategoryID,
                i.SubCategoryTitleAr,
                i.SubCategoryTitleEn,
                i.SubCategoryPic,
                i.SortOrder,
                i.Tags,
                i.Description,
                i.Category
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "SubCategoryID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(subcategories, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new SubCategory();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.SubCategories.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.SubCategoryID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.SubCategories.FirstOrDefaultAsync(item => item.SubCategoryID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.SubCategories.FirstOrDefaultAsync(item => item.SubCategoryID == key);

            _context.SubCategories.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CategoriesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Categories
                         orderby i.CategoryTitleAr
                         select new {
                             Value = i.CategoryId,
                             Text = i.CategoryTitleAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(SubCategory model, IDictionary values) {
            string SUB_CATEGORY_ID = nameof(SubCategory.SubCategoryID);
            string SUB_CATEGORY_TITLE_AR = nameof(SubCategory.SubCategoryTitleAr);
            string SUB_CATEGORY_TITLE_EN = nameof(SubCategory.SubCategoryTitleEn);
            string SUB_CATEGORY_PIC = nameof(SubCategory.SubCategoryPic);
            string SORT_ORDER = nameof(SubCategory.SortOrder);
            string TAGS = nameof(SubCategory.Tags);
            string DESCRIPTION = nameof(SubCategory.Description);
            string CATEGORY_ID = nameof(SubCategory.CategoryId);

            if(values.Contains(SUB_CATEGORY_ID)) {
                model.SubCategoryID = Convert.ToInt32(values[SUB_CATEGORY_ID]);
            }

            if(values.Contains(SUB_CATEGORY_TITLE_AR)) {
                model.SubCategoryTitleAr = Convert.ToString(values[SUB_CATEGORY_TITLE_AR]);
            }

            if(values.Contains(SUB_CATEGORY_TITLE_EN)) {
                model.SubCategoryTitleEn = Convert.ToString(values[SUB_CATEGORY_TITLE_EN]);
            }

            if(values.Contains(SUB_CATEGORY_PIC)) {
                model.SubCategoryPic = Convert.ToString(values[SUB_CATEGORY_PIC]);
            }

            if(values.Contains(SORT_ORDER)) {
                model.SortOrder = values[SORT_ORDER] != null ? Convert.ToInt32(values[SORT_ORDER]) : (int?)null;
            }

            if(values.Contains(TAGS)) {
                model.Tags = Convert.ToString(values[TAGS]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(CATEGORY_ID)) {
                model.CategoryId = Convert.ToInt32(values[CATEGORY_ID]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}
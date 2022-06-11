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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CategoriesController : Controller
    {
        private IcityContext _context;

        public CategoriesController(IcityContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var categories = _context.Categories.Select(i => new {
                i.CategoryId,
                i.CategoryTitleAr,
                i.CategoryTitleEn,
                i.CategoryPic,
                i.Description
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CategoryId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(categories, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Category();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Categories.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CategoryId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Categories.FirstOrDefaultAsync(item => item.CategoryId == key);
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
            var model = await _context.Categories.FirstOrDefaultAsync(item => item.CategoryId == key);

            _context.Categories.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Category model, IDictionary values) {
            string CATEGORY_ID = nameof(Category.CategoryId);
            string CATEGORY_TITLE_AR = nameof(Category.CategoryTitleAr);
            string CATEGORY_TITLE_EN = nameof(Category.CategoryTitleEn);
            string CATEGORY_PIC = nameof(Category.CategoryPic);
            string DESCRIPTION = nameof(Category.Description);

            if(values.Contains(CATEGORY_ID)) {
                model.CategoryId = Convert.ToInt32(values[CATEGORY_ID]);
            }

            if(values.Contains(CATEGORY_TITLE_AR)) {
                model.CategoryTitleAr = Convert.ToString(values[CATEGORY_TITLE_AR]);
            }

            if(values.Contains(CATEGORY_TITLE_EN)) {
                model.CategoryTitleEn = Convert.ToString(values[CATEGORY_TITLE_EN]);
            }

            if(values.Contains(CATEGORY_PIC)) {
                model.CategoryPic = Convert.ToString(values[CATEGORY_PIC]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
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
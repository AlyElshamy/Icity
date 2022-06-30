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
    public class ClassifiedAdsTypesController : Controller
    {
        private IcityContext _context;

        public ClassifiedAdsTypesController(IcityContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var classifiedadstypes = _context.ClassifiedAdsTypes.Select(i => new {
                i.ClassifiedAdsTypeID,
                i.TypeTitleAr,
                i.TypeTitleEn,
                i.TypePic,
                i.SortOrder
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ClassifiedAdsTypeID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(classifiedadstypes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ClassifiedAdsType();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ClassifiedAdsTypes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ClassifiedAdsTypeID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.ClassifiedAdsTypes.FirstOrDefaultAsync(item => item.ClassifiedAdsTypeID == key);
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
            var model = await _context.ClassifiedAdsTypes.FirstOrDefaultAsync(item => item.ClassifiedAdsTypeID == key);

            _context.ClassifiedAdsTypes.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ClassifiedAdsType model, IDictionary values) {
            string CLASSIFIED_ADS_TYPE_ID = nameof(ClassifiedAdsType.ClassifiedAdsTypeID);
            string TYPE_TITLE_AR = nameof(ClassifiedAdsType.TypeTitleAr);
            string TYPE_TITLE_EN = nameof(ClassifiedAdsType.TypeTitleEn);
            string TYPE_PIC = nameof(ClassifiedAdsType.TypePic);
            string SORT_ORDER = nameof(ClassifiedAdsType.SortOrder);

            if(values.Contains(CLASSIFIED_ADS_TYPE_ID)) {
                model.ClassifiedAdsTypeID = Convert.ToInt32(values[CLASSIFIED_ADS_TYPE_ID]);
            }

            if(values.Contains(TYPE_TITLE_AR)) {
                model.TypeTitleAr = Convert.ToString(values[TYPE_TITLE_AR]);
            }

            if(values.Contains(TYPE_TITLE_EN)) {
                model.TypeTitleEn = Convert.ToString(values[TYPE_TITLE_EN]);
            }

            if(values.Contains(TYPE_PIC)) {
                model.TypePic = Convert.ToString(values[TYPE_PIC]);
            }

            if(values.Contains(SORT_ORDER)) {
                model.SortOrder = values[SORT_ORDER] != null ? Convert.ToInt32(values[SORT_ORDER]) : (int?)null;
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
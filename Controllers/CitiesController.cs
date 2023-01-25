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
    public class CitiesController : Controller
    {
        private IcityContext _context;

        public CitiesController(IcityContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var cities = _context.Cities.Select(i => new {
                i.CityId,
                i.CityTlAr,
                i.CityTlEn,
                i.CountryId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CityId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(cities, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new City();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Cities.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CityId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Cities.FirstOrDefaultAsync(item => item.CityId == key);
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
            var model = await _context.Cities.FirstOrDefaultAsync(item => item.CityId == key);

            _context.Cities.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CountriesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Countries
                         orderby i.CountryTlAr
                         select new {
                             Value = i.CountryId,
                             Text = i.CountryTlAr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(City model, IDictionary values) {
            string CITY_ID = nameof(City.CityId);
            string CITY_TL_AR = nameof(City.CityTlAr);
            string CITY_TL_EN = nameof(City.CityTlEn);
            string COUNTRY_ID = nameof(City.CountryId);

            if(values.Contains(CITY_ID)) {
                model.CityId = Convert.ToInt32(values[CITY_ID]);
            }

            if(values.Contains(CITY_TL_AR)) {
                model.CityTlAr = Convert.ToString(values[CITY_TL_AR]);
            }

            if(values.Contains(CITY_TL_EN)) {
                model.CityTlEn = Convert.ToString(values[CITY_TL_EN]);
            }

            if(values.Contains(COUNTRY_ID)) {
                model.CountryId = Convert.ToInt32(values[COUNTRY_ID]);
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
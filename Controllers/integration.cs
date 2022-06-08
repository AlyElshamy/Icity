using Pharmacy.Data;
using Pharmacy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Entities;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Pharmacy.Controllers
{
    public class integration : Controller
    {
        private readonly PharmacyContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;


        //Items Page
        [HttpGet]
        public async Task<IActionResult> GetItemsBySectionID([FromQuery] int SectionId)
        {
            var ItemsList = await _context.Items.Where(c => c.ItemId == SectionId).ToListAsync();

            return Ok(new { ItemsList });
        }
        [HttpGet]
        public async Task<IActionResult> GetItemsList()
        {
            var ItemsList = await _context.Items.ToListAsync();
            return Ok(new { ItemsList });
        }



        [HttpGet]
        public async Task<IActionResult> GetItemsByCategory ([FromQuery] int CategoryId, [FromQuery] int CustomerId)
        {
            if (CustomerId == 0)
            {
                var items = _context.Items.Where(c => c.CategoryId == CategoryId).Select(i => new
                {
                    i.ItemId,
                    i.ItemTlAr,
                    i.ItemTlEn,
                    i.ItemPic,
                    i.Price,
                    i.BrandId,
                    i.Brand.BrandTlAr,
                    i.Brand.BrandTlEn,
                    i.CategoryId,
                    i.Category.SectionId,
                    i.Description,
                    i.Remarks,
                    i.IsActive,
                    //itemImage = i.ItemImage,

                    IsFav = false
                });
                return Ok(new { items });

            }
            else
            {
                var items = _context.Items.Where(c => c.CategoryId == CategoryId).Select(i => new
                {
                    i.ItemId,
                    i.ItemTlAr,
                    i.ItemTlEn,
                    i.ItemPic,
                    i.Price,
                    i.BrandId,
                    i.Brand.BrandTlAr,
                    i.Brand.BrandTlEn,
                    i.Description,
                    i.CategoryId,
                    i.Category.SectionId,
                    i.Remarks,
                    i.IsActive,
                    //itemImage = i.ItemImage,
                    IsFav = _context.CustomerFav.Any(c => c.CustomerId == CustomerId && c.ItemId == i.ItemId)
                });
                return Ok(new { items });

            }
        }

    }
}

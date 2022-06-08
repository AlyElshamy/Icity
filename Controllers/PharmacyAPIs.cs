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
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Pharmacy.Controllers
{

    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class PharmacyAPIsController : Controller
    {
        private readonly PharmacyContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IEmailSender _emailSender;


        public PharmacyAPIsController(PharmacyContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
           ApplicationDbContext db, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment, IEmailSender emailSender)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
            _hostEnvironment = hostEnvironment;
            _emailSender = emailSender;


        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel Model)
        {
            var userExists = await _userManager.FindByNameAsync(Model.UserName);
            if (userExists != null)

                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            var customer = new Customer();
            if (Model.CustomerImage != null)
            {
                var bytes = Convert.FromBase64String(Model.CustomerImage);
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Customer");
                string uniqePictureName = Guid.NewGuid().ToString("N") + ".jpeg";
                string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
                using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
                customer.CustomerImage = uniqePictureName;
            }
            customer.CustomerAddress = Model.CustomerAddress;
            customer.CustomerNameAr = Model.CustomerNameAr;
            customer.CustomerNameEn = Model.CustomerNameEn;
            customer.CustomerPhone = Model.CustomerPhone;
            customer.CustomerRemarks = Model.CustomerRemarks;
            customer.CustomerRemarks = Model.CustomerRemarks;
            customer.CustomerEmail = Model.CustomerEmail;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            if (!(customer.CustomerId > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            }


            var user = new ApplicationUser
            {
                UserName = Model.UserName,
                Email = Model.CustomerEmail,
                PhoneNumber = Model.CustomerPhone,
                EntityId = customer.CustomerId,
                EntityName = 2

            };

            var result = await _userManager.CreateAsync(user, Model.Password);

            if (!result.Succeeded)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            }
            await _userManager.AddToRoleAsync(user, "Customer");
            return Ok(new { Status = "Success", Message = "User created successfully!", user, customer });

        }


        [HttpGet]
        public async Task<IActionResult> GetCategoriesList()
        {
            var categoryList = await _context.Categories.ToListAsync();
            return Ok(new { categoryList });
        }


        [HttpGet]
        public async Task<IActionResult> Search ([FromQuery] string SearchText, [FromQuery] int CustomerId)

        {
            if (CustomerId == 0)
            {

                var items = _context.Items.Where(c => c.ItemTlAr.Contains(SearchText) || c.ItemTlEn.Contains(SearchText) || c.Category.CategoryTlAr.Contains(SearchText) || c.Category.CategoryTlEn.Contains(SearchText) || c.Description.Contains(SearchText) || c.Brand.BrandTlAr.Contains(SearchText) || c.Brand.BrandTlEn.Contains(SearchText)).Select(i => new
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
                    IsFav = false
                });
                return Ok(new { items });
            }
            else
            {
                var items = _context.Items.Where(c => c.ItemTlAr.Contains(SearchText) || c.ItemTlEn.Contains(SearchText) || c.Category.CategoryTlAr.Contains(SearchText) || c.Category.CategoryTlEn.Contains(SearchText) || c.Description.Contains(SearchText) || c.Brand.BrandTlAr.Contains(SearchText) || c.Brand.BrandTlEn.Contains(SearchText)).Select(i => new
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
                    IsFav = _context.CustomerFav.Any(c => c.CustomerId == CustomerId && c.ItemId == i.ItemId)
                });
                return Ok(new { items });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetHomeSlider()
        {

            var sliderList = await _context.HomeSliders.ToListAsync();

            return Ok(new { sliderList });
        }




        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var BrandList = await _context.Brands.ToListAsync();
            return Ok(new { BrandList });
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomerInfo([FromQuery] int CustomerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
            if (customer == null)
            {
                return Ok(new { Message = "customer Not Found" });
            }
            return Ok(new { customer });
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationUser>> Login([FromQuery] string UserName, [FromQuery] string Password)
        {

            var user = await _userManager.FindByNameAsync(UserName);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, Password, true);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null && roles.FirstOrDefault() == "Customer")
                    {
                        var customer = await _context.Customers.FindAsync(user.EntityId);

                        return Ok(new { Status = "Success", Message = "User Login successfully!", user, customer });
                    }
                }
            }
            var invalidResponse = new { status = false };
            return Ok(invalidResponse);
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> forgetPassword([FromQuery] string UserName)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(UserName);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] Cart model)
        {
            model.CartDate = DateTime.Now;
            _context.Carts.Add(model);
            await _context.SaveChangesAsync();
            return Ok(new { Message = " Item Added To Cart" });
        }

        [HttpPost]
        public async Task<IActionResult> deleteCartItem(int id)
        {
            Cart cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return Ok(new { Message = " Item Deleted" });
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentList()
        {
            List<PaymentMethod> payments = await _context.PaymentMethods.ToListAsync();

            return Ok(new { payments });
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderList(int userId)
        {
            List<Order> lstOrders = new List<Order>();
            var user = await _context.Customers.FindAsync(userId);

            lstOrders = await _context.Orders.Where(o => o.CustomerId == userId).ToListAsync();
            return Ok(new { lstOrders });
        }
        [HttpGet]
        public async Task<IActionResult> allOrderList()
        {
            List<Order> lstOrders = new List<Order>();
            lstOrders = await _context.Orders.ToListAsync();
            return Ok(new { lstOrders });
        }
        [HttpPost]
        public async Task<IActionResult> makeOrder([FromBody] Order model)
        {
            model.OrderDate = DateTime.Now;
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
            return Ok(new { Message = " Order Added" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] Cart model)
        {
            var cartModel = await _context.Carts.AnyAsync(c => c.CartId == model.CartId);
            if (cartModel == false)
            {
                return Ok(new { Message = "  Item Not Found" });
            }
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Item Updated" });

        }








    }
    }

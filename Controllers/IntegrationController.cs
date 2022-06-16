using Icity.Data;
using Icity.Entities;
using Icity.Models;
using Icity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Icity.Controllers
{
    [Route("api/[controller]/[action]")]
    public class IntegrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IcityContext _context { get; set; }
        public ApplicationDbContext _applicationDbContext  { get; set; }
        private readonly IEmailSender _emailSender; 
        public IntegrationController(ApplicationDbContext applicationDbContext ,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IcityContext Context, IWebHostEnvironment hostEnvironment, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _emailSender = emailSender;
            _context = Context;
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<ApplicationUser>> Login(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    return Ok(new { status = false, message="Email Not Confirmed" });
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, Password, true);
                if (result.Succeeded)
                {
                    return Ok(new { Status = "Success", User=user });
                }
            }
            var invalidResponse = new { status = false };
            return Ok(invalidResponse);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel Model)
        {
            var userExists = await _userManager.FindByEmailAsync(Model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            var user = new ApplicationUser
            {
                FullName = Model.FullName,
                Email = Model.Email,
                PhoneNumber = Model.Phone,
                UserName = Model.Email
            };
            var result = await _userManager.CreateAsync(user, Model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("GetConfirmEmail", "Integration",new { Email = user.Email },Request.Scheme, "codewarenet-001-site14.dtempurl.com");
            await _emailSender.SendEmailAsync(Model.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            return Ok(new { Status = "Success", Message = "User created successfully!", user });
        }
        [HttpGet]
        public async Task<IActionResult> GetConfirmEmail(string Email)
        {
            if (Email==null)
            {
                return BadRequest("Enter User Id..");
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(IFormFile Profilepic, IFormFile bannerpic, IFormFileCollection Images, IFormFileCollection VideosFiels, UserProfile userProfile)
        {
          

            try
            {
                var user = await _userManager.FindByEmailAsync(userProfile.Email);
                if (user == null)
                    return BadRequest("Email Not Found..");
                user.Gender = userProfile.Gender;
                user.Bio = userProfile.Bio;
                user.BirthDate = userProfile.BirthDate;
                user.Job = userProfile.Job;
                user.Qualification = userProfile.Qualification;
                //user.Location = userProfile.Location;
                user.FullName = userProfile.FullName;
                user.PhoneNumber = userProfile.Phone;
                user.FacebookLink = userProfile.FacebookLink;
                user.TwitterLink = userProfile.TwitterLink;
                user.InstagramLink = userProfile.InstagramLink;
                user.LinkedInLink = userProfile.LinkedInLink;
                user.LinkedInLink = userProfile.LinkedInLink;
                user.NickName = userProfile.NickName;
                user.MaritalStatus = userProfile.MaritalStatus;
                user.City = userProfile.City;
                user.MapLocation = userProfile.MapLocation;
                user.Country = userProfile.Country;
                user.Phone2 = userProfile.Phone2;
                user.Folwers = userProfile.Folwers;
                user.Website = userProfile.Website;
                user.Photos = userProfile.Photos==null? new List<Photo>() : userProfile.Photos;
                user.Videos = userProfile.Videos==null? new List<Video>() : userProfile.Videos;
                user.Skills = userProfile.Skills==null? new List<Skill>() : userProfile.Skills;
                user.Interests = userProfile.Interests == null ? new List<Interest>() : userProfile.Interests;
                user.Educations = userProfile.Skills == null ? new List<Education>() : userProfile.Educations;
                user.LifeEvents = userProfile.LifeEvents == null ? new List<LifeEvent>() : userProfile.LifeEvents; ;
                user.Languages = userProfile.Languages == null ? new List<Language>() : userProfile.Languages; ;

                if (Images.Count() > 0&& user.Photos.Count()==Images.Count())
                {
                    for (int i = 0; i < Images.Count(); i++)
                    {
                        Photo photo = new Photo();
                        if (Images[i] != null)
                        {
                            string folder = "Images/ProfileImages/";
                            photo.Image = UploadImage(folder, Images[i]);
                            photo.PublishDate = userProfile.Photos[i].PublishDate;
                            photo.Caption = userProfile.Photos[i].Caption;
                            photo.Id = user.Id;


                        }
                        _applicationDbContext.Photos.Add(photo);


                    }
                }
                if (VideosFiels.Count() > 0&& user.Videos.Count() == VideosFiels.Count())
                {
                    for (int i = 0; i < VideosFiels.Count(); i++)
                    {
                        Video video = new Video();
                        if (VideosFiels[i] != null)
                        {
                            string folder = "Videos/ProfileVideos/";
                            video.VideoT = UploadImage(folder, VideosFiels[i]);
                            video.Caption = userProfile.Videos[i].Caption;
                            video.PublishDate = userProfile.Videos[i].PublishDate;
                            video.Id = user.Id;


                        }
                        _applicationDbContext.Videos.Add(video);


                    }
                }

                _applicationDbContext.SaveChanges();

                if (Profilepic != null)
                {
                    string folder = "Images/ProfileImages/";
                    user.ProfilePicture = UploadImage(folder, Profilepic);
                }
                if (bannerpic != null)
                {
                    string folder = "Images/BannerImages/";
                    user.Profilebanner = UploadImage(folder, bannerpic);
                }
                await _userManager.UpdateAsync(user);
                return Ok("updated successfully.. ");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile(string userid)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userid);   
                
                if (user != null)
                {
                    user.Photos = _applicationDbContext.Photos.Where(e => e.Id == userid).ToList(); 
                    user.Videos = _applicationDbContext.Videos.Where(e => e.Id == userid).ToList(); 
                    user.Languages = _applicationDbContext.Languages.Where(e => e.Id == userid).ToList();
                    user.Interests = _applicationDbContext.Interests.Where(e => e.Id == userid).ToList() ;
                    user.Skills = _applicationDbContext.Skills.Where(e => e.Id == userid).ToList(); 
                    user.LifeEvents = _applicationDbContext.LifeEvents.Where(e => e.Id == userid).ToList(); 
                    user.Educations = _applicationDbContext.Educations.Where(e => e.Id == userid).ToList(); 
               
                    return Ok(user);

                }
                return BadRequest("User Not Found");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories =await _context.Categories.ToListAsync();
                var model = new
                {
                    status = true,
                    Categories = categories
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetCategoryByID(int CategoryId)
        {
            if (CategoryId != 0)
            {
                try
                {
                    var categories = _context.Categories.Find(CategoryId);
                    if (categories != null)
                    {
                        return Ok(categories);
                    }
                    return BadRequest("Category NotFound..");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Valid ID..");

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool validateCategory(Category category)
        {
            if (category.CategoryTitleAr != null && category.CategoryTitleEn != null)
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        public IActionResult AddCategory(IFormFile categoryPic, Category category)
        {
            if (categoryPic == null)
            {
                return BadRequest("Enter Category Photo..");
            }
            if (validateCategory(category))
            {
                try
                {
                    string folder = "Images/Category/";
                    category.CategoryPic = UploadImage(folder, categoryPic);
                    _context.Categories.Add(category);
                    _context.SaveChanges();
                    return Created("Successfully Add Category..", category);
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Arabic And English Category Title..");
        }
        [HttpPut]
        public IActionResult EditCategory(Category category, IFormFile categoryPic)
        {
            var categoryobj = _context.Categories.Where(a=>a.CategoryId==category.CategoryId).FirstOrDefault();
            if (categoryobj == null)
            {
                return BadRequest("Object NotFound..");
            }
            if (validateCategory(category))
            {
                try
                {
                    if (categoryPic != null)
                    {
                        var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, category.CategoryPic);
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                        string folder = "Images/Category/";
                        categoryobj.CategoryPic = UploadImage(folder, categoryPic);
                    }
                    categoryobj.Description = category.Description;
                    categoryobj.CategoryTitleAr = category.CategoryTitleAr;
                    categoryobj.CategoryTitleEn = category.CategoryTitleEn;
                    categoryobj.Tags = category.Tags;
                    categoryobj.SortOrder = category.SortOrder;

                    
                    _context.Attach(categoryobj).State = EntityState.Modified;
                    // await TryUpdateModelAsync<Category>(categoryobj, "",a=>a.CategoryPic,a=>category.CategoryTitleAr,a=> category.CategoryTitleEn,a=> category.Description);
                    //_context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    return Ok("Successfully Edited Category..");
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter All Required Data..");
            }

        [HttpDelete]
        public IActionResult DeleteCategory(int CategoryId)
        {
            if (CategoryId != 0)
            {
                try
                {
                    var categories = _context.Categories.Find(CategoryId);
                    if (categories != null)
                    {
                        _context.Categories.Remove(categories);
                        _context.SaveChanges();
                        return Ok("Category Deleted Successfully..");
                    }
                    return BadRequest("Category NotFound..");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Valid ID..");

        }


    }
}


using Icity.Data;
using Icity.Entities;
using Icity.Models;
using Icity.ViewModel;
using Icity.ViewModels;
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
        public ApplicationDbContext _applicationDbContext { get; set; }
        private readonly IEmailSender _emailSender;
        public IntegrationController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IcityContext Context, IWebHostEnvironment hostEnvironment, IEmailSender emailSender)
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
                    return Ok(new { status = false, message = "Email Not Confirmed" });
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, Password, true);
                if (result.Succeeded)
                {
                    return Ok(new { Status = "Success", User = user });
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
            var callbackUrl = Url.Action("GetConfirmEmail", "Integration", new { Email = user.Email }, Request.Scheme, "codewarenet-001-site14.dtempurl.com");
            await _emailSender.SendEmailAsync(Model.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            return Ok(new { Status = "Success", Message = "User created successfully!", user });
        }
        [HttpGet]
        public async Task<IActionResult> GetConfirmEmail(string Email)
        {
            if (Email == null)
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

        [HttpGet]
        public async Task<IActionResult> GetAllEducation(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var educationList = _applicationDbContext.Educations.Where(e => e.Id == user.Id);
                return Ok(new { Status = "Success", EducationList = educationList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateEducationModel(Education education)
        {
            if (education.Year == 0 || education.Provider == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> AddEducation(Education education, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidateEducationModel(education))
                {
                    return Ok(new { Status = "false", Reason = "Enter All Required Faild" });
                }
                education.Id = user.Id;
                _applicationDbContext.Educations.Add(education);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", Education = education });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpDelete]
        public IActionResult DeleteEducation(int educationId)
        {
            if (educationId == 0)
            {
                return Ok(new { Status = "false", Reason = "Invalid Education Id" });
            }
            try
            {
                var educationObj = _applicationDbContext.Educations.Where(e => e.EducationID == educationId).FirstOrDefault();
                if (educationObj == null)
                {
                    return Ok(new { Status = "false", Reason = "Object Not Found" });
                }

                _applicationDbContext.Educations.Remove(educationObj);
                _applicationDbContext.SaveChanges();

                return Ok(new { Status = "Success", Message = "Education Deleted Successfully" });


            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateSkillModel(Skill skill)
        {
            if (skill.SkillTitle == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> AddSkill(Skill skill, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidateSkillModel(skill))
                {
                    return Ok(new { Status = "false", Reason = "Enter Skill Title" });
                }
                skill.Id = user.Id;
                _applicationDbContext.Skills.Add(skill);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", Skill = skill });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllSkills(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var skillList = _applicationDbContext.Skills.Where(e => e.Id == user.Id);
                return Ok(new { Status = "Success", SkillList = skillList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteSkill(int skillId)
        {
            if (skillId == 0)
            {
                return Ok(new { Status = "false", Reason = "Invalid Skill Id" });
            }
            try
            {
                var skillObj = _applicationDbContext.Skills.Where(e => e.SkillID == skillId).FirstOrDefault();
                if (skillObj == null)
                {
                    return Ok(new { Status = "false", Reason = "Object Not Found" });
                }

                _applicationDbContext.Skills.Remove(skillObj);
                _applicationDbContext.SaveChanges();

                return Ok(new { Status = "Success", Message = "Skill Deleted Successfully" });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateLanguageModel(Language language)
        {
            if (language.LanguageTitle == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> AddLanguage(Language language, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidateLanguageModel(language))
                {
                    return Ok(new { Status = "false", Reason = "Enter Language Title" });
                }
                language.Id = user.Id;
                _applicationDbContext.Languages.Add(language);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", Language = language });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllLanguages(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var languagesList = _applicationDbContext.Languages.Where(e => e.Id == user.Id);
                return Ok(new { Status = "Success", LanguagesList = languagesList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateLifeEventModel(LifeEvent lifeEvent)
        {
            if (lifeEvent.Caption == null || lifeEvent.EventType == null)
            {
                return false;
            }
            return true;
        }
        [HttpDelete]
        public IActionResult DeleteLanguage(int languageId)
        {
            if (languageId == 0)
            {
                return Ok(new { Status = "false", Reason = "Invalid Language Id" });
            }
            try
            {
                var languageObj = _applicationDbContext.Languages.Where(e => e.LanguageID == languageId).FirstOrDefault();
                if (languageObj == null)
                {
                    return Ok(new { Status = "false", Reason = "Object Not Found" });
                }

                _applicationDbContext.Languages.Remove(languageObj);
                _applicationDbContext.SaveChanges();

                return Ok(new { Status = "Success", Message = "Language Deleted Successfully" });


            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddLifeEvent(LifeEvent lifeEvent, IFormFile media, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidateLifeEventModel(lifeEvent))
                {
                    return Ok(new { Status = "false", Reason = "Enter All Required Fields" });
                }
                if (media == null)
                {
                    return Ok(new { Status = "false", Reason = "Enter Life Event Media" });
                }
                if (media != null)
                {
                    string folder = "Images/ProfileImages/";
                    lifeEvent.Media = UploadImage(folder, media);
                }

                lifeEvent.Id = user.Id;
                _applicationDbContext.LifeEvents.Add(lifeEvent);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", LifeEvent = lifeEvent });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllLifeEvents(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var lifeEventsList = _applicationDbContext.LifeEvents.Where(e => e.Id == user.Id);
                return Ok(new { Status = "Success", LifeEventsList = lifeEventsList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteLifeEvent(int lifeEventId)
        {
            if (lifeEventId == 0)
            {
                return Ok(new { Status = "false", Reason = "Invalid LifeEvent Id" });
            }
            try
            {
                var lifeEventObj = _applicationDbContext.LifeEvents.Where(e => e.LifeEventID == lifeEventId).FirstOrDefault();
                if (lifeEventObj == null)
                {
                    return Ok(new { Status = "false", Reason = "Object Not Found" });
                }

                _applicationDbContext.LifeEvents.Remove(lifeEventObj);
                _applicationDbContext.SaveChanges();

                return Ok(new { Status = "Success", Message = "Life Event Deleted Successfully" });


            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateInterestModel(Interest interest)
        {
            if (interest.InterestTitle == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> AddInterest(Interest interest, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidateInterestModel(interest))
                {
                    return Ok(new { Status = "false", Reason = "Enter Interest Title" });
                }


                interest.Id = user.Id;
                _applicationDbContext.Interests.Add(interest);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", Interest = interest });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllInterest(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var interestsList = _applicationDbContext.Interests.Where(e => e.Id == user.Id);
                return Ok(new { Status = "Success", InterestsList = interestsList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteInterest(int interestId)
        {
            if (interestId == 0)
            {
                return Ok(new { Status = "false", Reason = "Invalid interest Id" });
            }
            try
            {
                var interestObj = _applicationDbContext.Interests.Where(e => e.InterestID == interestId).FirstOrDefault();
                if (interestObj == null)
                {
                    return Ok(new { Status = "false", Reason = "Object Not Found" });
                }

                _applicationDbContext.Interests.Remove(interestObj);
                _context.SaveChanges();
                return Ok(new { Status = "Success", Message = "Interest Deleted Successfully" });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]

        private bool ValidatePhotoModel(Photo photo)
        {
            if (photo.Caption == null)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(Photo photo, IFormFile image, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidatePhotoModel(photo))
                {
                    return Ok(new { Status = "false", Reason = "Enter Photo Caption" });
                }
                if (image == null)
                {
                    return Ok(new { Status = "false", Reason = "Plz Upload Image File" });
                }
                if (image != null)
                {
                    string folder = "Images/ProfileImages/";
                    photo.Image = UploadImage(folder, image);
                }

                photo.Id = user.Id;
                _applicationDbContext.Photos.Add(photo);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", Photo = photo });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhotos(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var photosList = _applicationDbContext.Photos.Where(e => e.Id == user.Id);

                return Ok(new { Status = "Success", PhotosList = photosList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpGet]
        //public async Task<IActionResult> GetAllMedia(string userEmail)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(userEmail);
        //        if (user == null)
        //        {
        //            return Ok(new { Status = "false", Reason = "User Not Found" });
        //        }
        //        var media = new List<MediaVM>();

        //        var photosList = _applicationDbContext.Photos.Where(e => e.Id == user.Id);
        //        var VideosList = _applicationDbContext.Videos.Where(e => e.Id == user.Id);
        //        if (photosList != null)
        //        {
        //            foreach (var item in photosList)
        //            {
        //                var mediaobj = new MediaVM() { Caption = item.Caption, MediaURL = item.Image, PublishDate = item.PublishDate };
        //                media.Add(mediaobj);
        //            }
        //        }
        //        if (VideosList != null)
        //        {
        //            foreach (var item in VideosList)
        //            {
        //                var mediaobj = new MediaVM() { Caption = item.Caption, MediaURL = item.VideoT, PublishDate = item.PublishDate };
        //                media.Add(mediaobj);
        //            }
        //        }
        //        if (media != null)
        //        {
        //            media = media.OrderBy(a => a.PublishDate).ToList();
        //        }

        //        return Ok(new { Status = "Success", Media = media });

        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(new { Status = "false", Reason = e.Message });
        //    }
        //}
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidatevideoModel(Video video)
        {
            if (video.Caption == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> AddVideo(Video video, IFormFile VideoFile, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                if (!ValidatevideoModel(video))
                {
                    return Ok(new { Status = "false", Reason = "Enter Photo Caption" });
                }
                if (VideoFile == null)
                {
                    return Ok(new { Status = "false", Reason = "Plz Upload Video File" });
                }
                if (VideoFile != null)
                {
                    string folder = "Images/ProfileImages/";
                    video.VideoT = UploadImage(folder, VideoFile);
                }

                video.Id = user.Id;

                _applicationDbContext.Videos.Add(video);
                _applicationDbContext.SaveChanges();
                return Ok(new { Status = "Success", Video = video });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllVideos(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }

                var videosList = _applicationDbContext.Videos.Where(e => e.Id == user.Id);
                return Ok(new { Status = "Success", VideosList = videosList });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateUserProfile(IFormFile Profilepic, IFormFile bannerpic, IFormFileCollection Images, IFormFileCollection VideosFiels, List<IFormFile> EventsMedia, UserProfile userProfile)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(userProfile.Email);
        //        if (user == null)
        //            return BadRequest("Email Not Found..");
        //        user.Gender = userProfile.Gender;
        //        user.Bio = userProfile.Bio;
        //        user.Nationality = userProfile.Nationality;
        //        user.BirthDate = userProfile.BirthDate;
        //        user.Job = userProfile.Job;
        //        user.Qualification = userProfile.Qualification;
        //        //user.Location = userProfile.Location;
        //        user.FullName = userProfile.FullName;
        //        user.PhoneNumber = userProfile.Phone;
        //        user.FacebookLink = userProfile.FacebookLink;
        //        user.TwitterLink = userProfile.TwitterLink;
        //        user.InstagramLink = userProfile.InstagramLink;
        //        user.LinkedInLink = userProfile.LinkedInLink;
        //        user.LinkedInLink = userProfile.LinkedInLink;
        //        user.NickName = userProfile.NickName;
        //        user.MaritalStatus = userProfile.MaritalStatus;
        //        user.City = userProfile.City;
        //        user.MapLocation = userProfile.MapLocation;
        //        user.Country = userProfile.Country;
        //        user.Phone2 = userProfile.Phone2;
        //        user.Folwers = userProfile.Folwers;
        //        user.Website = userProfile.Website;
        //        user.YoutubeLink = userProfile.YoutubeLink;
        //        user.Photos = userProfile.Photos == null ? new List<Photo>() : userProfile.Photos;
        //        user.Videos = userProfile.Videos == null ? new List<Video>() : userProfile.Videos;
        //        var skillslist = _applicationDbContext.Skills.Where(a => a.Id == user.Id).ToList();
        //        _applicationDbContext.Skills.RemoveRange(skillslist);
        //        var Interistslist = _applicationDbContext.Interests.Where(a => a.Id == user.Id).ToList();
        //        _applicationDbContext.Interests.RemoveRange(Interistslist);
        //        var educationslist = _applicationDbContext.Educations.Where(a => a.Id == user.Id).ToList();
        //        _applicationDbContext.Educations.RemoveRange(educationslist);
        //        var languageslist = _applicationDbContext.Languages.Where(a => a.Id == user.Id).ToList();
        //        _applicationDbContext.Languages.RemoveRange(languageslist);

        //        user.Skills = userProfile.Skills == null ? new List<Skill>() : userProfile.Skills;
        //        user.Interests = userProfile.Interests == null ? new List<Interest>() : userProfile.Interests;
        //        user.Educations = userProfile.Skills == null ? new List<Education>() : userProfile.Educations;
        //        user.Languages = userProfile.Languages == null ? new List<Language>() : userProfile.Languages;



        //        int n = 0;
        //        var eventlist = _applicationDbContext.LifeEvents.Where(a => a.Id == user.Id).ToList();
        //        _applicationDbContext.RemoveRange(eventlist);

        //        if (userProfile.LifeEvents != null)
        //        {

        //            for (int i = 0; i < userProfile.LifeEvents.Count(); i++)
        //            {
        //                if (userProfile.LifeEvents[i].Media == null)
        //                {
        //                    string folder = "Images/ProfileImages/";
        //                    userProfile.LifeEvents[i].Media = UploadImage(folder, EventsMedia[n]);
        //                    n++;
        //                }
        //            }
        //            user.LifeEvents = userProfile.LifeEvents == null ? new List<LifeEvent>() : userProfile.LifeEvents;
        //        }

        //        if (Images.Count() > 0)
        //        {
        //            for (int i = 0; i < Images.Count(); i++)
        //            {
        //                Photo photo = new Photo();
        //                if (Images[i] != null)
        //                {
        //                    string folder = "Images/ProfileImages/";
        //                    photo.Image = UploadImage(folder, Images[i]);
        //                    photo.PublishDate = DateTime.Now;
        //                    //photo.Caption = userProfile.Photos[i].Caption;
        //                    photo.Id = user.Id;
        //                }
        //                _applicationDbContext.Photos.Add(photo);
        //            }
        //        }
        //        if (VideosFiels.Count() > 0)
        //        {
        //            for (int i = 0; i < VideosFiels.Count(); i++)
        //            {
        //                Video video = new Video();
        //                if (VideosFiels[i] != null)
        //                {
        //                    string folder = "Videos/ProfileVideos/";
        //                    video.VideoT = UploadImage(folder, VideosFiels[i]);
        //                    //video.Caption = userProfile.Videos[i].Caption;
        //                    video.PublishDate = DateTime.Now;
        //                    video.Id = user.Id;
        //                }
        //                _applicationDbContext.Videos.Add(video);
        //            }
        //        }
        //        _applicationDbContext.SaveChanges();

        //        if (Profilepic != null)
        //        {
        //            string folder = "Images/ProfileImages/";
        //            user.ProfilePicture = UploadImage(folder, Profilepic);
        //        }
        //        if (bannerpic != null)
        //        {
        //            string folder = "Images/BannerImages/";
        //            user.Profilebanner = UploadImage(folder, bannerpic);
        //        }
        //        await _userManager.UpdateAsync(user);
        //        return Ok("updated successfully.. ");
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(e.Message);
        //    }
        //}



        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(IFormFile Profilepic, IFormFile bannerpic, UserProfile userProfile)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userProfile.Email);
                if (user == null)
                    return BadRequest("Email Not Found..");
                user.Gender = userProfile.Gender;
                user.Bio = userProfile.Bio;
                user.Nationality = userProfile.Nationality;
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
                user.YoutubeLink = userProfile.YoutubeLink;
                user.NickName = userProfile.NickName;
                user.MaritalStatus = userProfile.MaritalStatus;
                user.City = userProfile.City;
                user.MapLocation = userProfile.MapLocation;
                user.Country = userProfile.Country;
                user.Phone2 = userProfile.Phone2;
                user.Folwers = userProfile.Folwers;
                user.Website = userProfile.Website;

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

        [ApiExplorerSettings(IgnoreApi = true)]
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

        [HttpDelete]
        public IActionResult DeletePhoto(int PhotoId)
        {
            try
            {
                var Photo = _applicationDbContext.Photos.Where(a => a.PhotoID == PhotoId).FirstOrDefault();
                _applicationDbContext.Photos.Remove(Photo);
                _applicationDbContext.SaveChanges();
                return Ok(new { status = "success", message = "Photo suuccesfully deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteVideo(int VideoId)
        {
            try
            {
                var Video = _applicationDbContext.Videos.Where(a => a.VideoID == VideoId).FirstOrDefault();
                _applicationDbContext.Videos.Remove(Video);
                _applicationDbContext.SaveChanges();
                return Ok(new { status = "success", message = "Video suuccesfully deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile(string userid)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userid);
                //var listings = _context.AddListings.Where(a => a.CreatedByUser == userid).ToList();
                //List<Datalist> listingmedia = new List<Datalist>();

                if (user != null)
                {
                    user.Photos = _applicationDbContext.Photos.Where(e => e.Id == userid).OrderBy(a => a.PublishDate).ToList();
                    user.Videos = _applicationDbContext.Videos.Where(e => e.Id == userid).OrderBy(a => a.PublishDate).ToList();
                    //foreach (var item in user.Photos)
                    //{
                    //    listingmedia.Add(new Datalist(item.PhotoID, item.Image));
                    //}
                    //foreach (var item in user.Videos)
                    //{
                    //    listingmedia.Add(new Datalist(item.VideoID, item.VideoT));
                    //}

                    user.Languages = _applicationDbContext.Languages.Where(e => e.Id == userid).ToList();
                    user.Interests = _applicationDbContext.Interests.Where(e => e.Id == userid).ToList();
                    user.Skills = _applicationDbContext.Skills.Where(e => e.Id == userid).ToList();
                    user.LifeEvents = _applicationDbContext.LifeEvents.Where(e => e.Id == userid).ToList();
                    user.Educations = _applicationDbContext.Educations.Where(e => e.Id == userid).ToList();
                    //bool IsFavourite = _context.FavouriteProfiles.Any(o => o.Id == userid && o.UserId == userAccountId);
                    //bool IsFolowing = _context.FolowProfile.Any(o=> o.Id == userid && o.UserId == userAccountId);

                    return Ok(user);

                }
                return BadRequest("User Not Found");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetUserProfileById(string userid, string userAccountId)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(userid);
        //        if (user != null)
        //        {
        //            user.Photos = _applicationDbContext.Photos.Where(e => e.Id == userid).OrderBy(a => a.PublishDate).ToList();
        //            user.Videos = _applicationDbContext.Videos.Where(e => e.Id == userid).OrderBy(a => a.PublishDate).ToList();
        //            user.Languages = _applicationDbContext.Languages.Where(e => e.Id == userid).ToList();
        //            user.Interests = _applicationDbContext.Interests.Where(e => e.Id == userid).ToList();
        //            user.Skills = _applicationDbContext.Skills.Where(e => e.Id == userid).ToList();
        //            user.LifeEvents = _applicationDbContext.LifeEvents.Where(e => e.Id == userid).ToList();
        //            user.Educations = _applicationDbContext.Educations.Where(e => e.Id == userid).ToList();
        //            bool IsFavourite = _context.FavouriteProfiles.Any(o => o.Id == userid && o.UserId == userAccountId);
        //            bool IsFolowing = _context.FolowProfile.Any(o => o.Id == userid && o.UserId == userAccountId);
        //            return Ok(new { Status = true, user = user, IsFavourite= IsFavourite, IsFolowing= IsFolowing });
        //        }
        //        return Ok(new { Status = false, Message = "User Not Found"});
                
        //    }
        //    catch (Exception e)
        //    {

        //        return Ok(new { Status = false, Message = e.Message });
            
        //}
        //}
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool validateSubCategory(SubCategory Subcategory)
        {
            if (Subcategory.SubCategoryTitleAr != null && Subcategory.SubCategoryTitleEn != null && Subcategory.CategoryId != 0)
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
            var categoryobj = _context.Categories.Where(a => a.CategoryId == category.CategoryId).FirstOrDefault();
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

        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int CategoryId)
        {
            try
            {
                var subCategories = await _context.SubCategories.Where(a => a.CategoryId == CategoryId).ToListAsync();
                var model = new
                {
                    status = true,
                    SubCategories = subCategories
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetSubCategoryByID(int SubCategoryId)
        {
            if (SubCategoryId != 0)
            {
                try
                {
                    var categories = _context.SubCategories.Where(a => a.SubCategoryID == SubCategoryId).Include(a => a.Category).Select(a => new { a.SubCategoryTitleAr, a.SubCategoryTitleEn, a.SortOrder, a.Tags, a.SubCategoryPic, a.Description, a.Category.CategoryTitleAr, a.Category.CategoryTitleEn, a.SubCategoryID });
                    if (categories != null)
                    {
                        return Ok(categories);
                    }
                    return BadRequest("Sub Category NotFound..");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Valid ID..");

        }
        [HttpPost]
        public IActionResult AddSubCategory(IFormFile SubcategoryPic, SubCategory Subcategory)
        {
            if (Subcategory == null)
            {
                return BadRequest("Enter Subcategory Photo..");
            }
            if (validateSubCategory(Subcategory))
            {
                try
                {
                    string folder = "Images/SubCategory/";
                    Subcategory.SubCategoryPic = UploadImage(folder, SubcategoryPic);
                    _context.SubCategories.Add(Subcategory);
                    _context.SaveChanges();
                    return Created("Successfully Add Subcategory..", Subcategory);
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Arabic And English Subcategory Title..");
        }
        [HttpPut]
        public IActionResult EditSubCategory(SubCategory Subcategory, IFormFile SubcategoryPic)
        {
            var subcategoryobj = _context.SubCategories.Where(a => a.SubCategoryID == Subcategory.SubCategoryID).FirstOrDefault();
            if (subcategoryobj == null)
            {
                return BadRequest("Object NotFound..");
            }
            if (validateSubCategory(Subcategory))
            {
                try
                {
                    if (SubcategoryPic != null)
                    {
                        var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, Subcategory.SubCategoryPic);
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                        string folder = "Images/SubCategory/";
                        subcategoryobj.SubCategoryPic = UploadImage(folder, SubcategoryPic);
                    }
                    subcategoryobj.Description = Subcategory.Description;
                    subcategoryobj.SubCategoryTitleAr = Subcategory.SubCategoryTitleAr;
                    subcategoryobj.SubCategoryTitleEn = Subcategory.SubCategoryTitleEn;
                    subcategoryobj.Tags = Subcategory.Tags;
                    subcategoryobj.SortOrder = Subcategory.SortOrder;


                    _context.Attach(subcategoryobj).State = EntityState.Modified;
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
        public IActionResult DeleteSubCategory(int subCategoryId)
        {
            if (subCategoryId != 0)
            {
                try
                {
                    var subcategorie = _context.SubCategories.Where(a => a.SubCategoryID == subCategoryId).FirstOrDefault();
                    if (subcategorie != null)
                    {
                        _context.SubCategories.Remove(subcategorie);
                        _context.SaveChanges();
                        return Ok("Subcategory Deleted Successfully..");
                    }
                    return BadRequest("Subcategory NotFound..");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Valid ID..");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllMedia(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return Ok(new { Status = "false", Reason = "User Not Found" });
                }
                var media = new List<MediaVM>();

                var photosList = _applicationDbContext.Photos.Where(e => e.Id == user.Id);
                var VideosList = _applicationDbContext.Videos.Where(e => e.Id == user.Id);
                if (photosList != null)
                {
                    foreach (var item in photosList)
                    {
                        var mediaobj = new MediaVM() { MediaId = item.PhotoID, Caption = item.Caption, MediaURL = item.Image, PublishDate = item.PublishDate };
                        media.Add(mediaobj);
                    }
                }
                if (VideosList != null)
                {
                    foreach (var item in VideosList)
                    {
                        var mediaobj = new MediaVM() { MediaId = item.VideoID, Caption = item.Caption, MediaURL = item.VideoT, PublishDate = item.PublishDate };
                        media.Add(mediaobj);
                    }
                }
                if (media != null)
                {
                    media = media.OrderBy(a => a.PublishDate).ToList();
                }

                return Ok(new { Status = "Success", Media = media });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpPost]
        public IActionResult AddBussinesPhotos(int listingId, IFormFile Photo, string caption)
        {
            try
            {

                var listing = _context.AddListings.Find(listingId);
                if (listing == null)
                {
                    return Ok(new { status = "false", message = "Addlisting Not Found.." });
                }
                if (Photo == null)
                {
                    return Ok(new { status = "false", message = "Please Upload Photo.." });
                }
                var photoobj = new ListingPhotos() { Caption = caption, PublishDate = DateTime.Now, AddListingId = listingId };
                string folder = "Images/ListingMedia/Photos/";
                photoobj.PhotoUrl = UploadImage(folder, Photo);
                _context.ListingPhotos.Add(photoobj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Listing photo Added successfully..", photo = photoobj });

            }
            catch (Exception e)
            {

                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult AddBussinesVideo(int listingId, IFormFile Video, string caption)
        {
            try
            {

                var listing = _context.AddListings.Find(listingId);
                if (listing == null)
                {
                    return Ok(new { status = "false", message = "Addlisting Not Found.." });
                }
                if (Video == null)
                {
                    return Ok(new { status = "false", message = "Please Upload Video.." });
                }
                var videoobj = new ListingVideos() { Caption = caption, PublishDate = DateTime.Now, AddListingId = listingId };
                string folder = "Images/ListingMedia/Videos/";
                videoobj.VideoUrl = UploadImage(folder, Video);
                _context.ListingVideos.Add(videoobj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Listing Video Added successfully..", Video = videoobj });

            }
            catch (Exception e)
            {

                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddListing(IFormFile Listinglogo, IFormFile PromoVideo, IFormFile listingbanner, IFormFileCollection Videos, IFormFileCollection Photos, AddListing addListing)
        {
            var user = await _userManager.FindByEmailAsync(addListing.CreatedByUser);

            if (user == null)
            {
                return Ok(new { status = "false", message = "User Not Found.." });
            }


            List<ListingPhotos> photolistings = new List<ListingPhotos>();
            if (Photos.Count() > 0)
            {
                for (int i = 0; i < Photos.Count(); i++)
                {
                    ListingPhotos photoobj = new ListingPhotos();
                    if (Photos[i] != null)
                    {
                        string folder = "Images/ListingMedia/Photos/";
                        photoobj.PhotoUrl = UploadImage(folder, Photos[i]);
                    }

                    photolistings.Add(photoobj);
                }
                addListing.ListingPhotos = photolistings;
            }


            List<ListingVideos> videoListing = new List<ListingVideos>();
            if (Videos.Count() > 0)
            {
                for (int i = 0; i < Videos.Count(); i++)
                {
                    ListingVideos videoobj = new ListingVideos();
                    if (Videos[i] != null)
                    {
                        string folder = "Images/ListingMedia/Videos/";
                        videoobj.VideoUrl = UploadImage(folder, Videos[i]);
                    }

                    videoListing.Add(videoobj);
                }
                addListing.ListingVideos = videoListing;
            }

            if (Listinglogo != null)
            {
                string folder = "Images/ListingMedia/Logos/";
                addListing.ListingLogo = UploadImage(folder, Listinglogo);
            }
            if (PromoVideo != null)
            {
                string folder = "Images/ListingMedia/Videos/";
                addListing.PromoVideo = UploadImage(folder, PromoVideo);
            }
            if (listingbanner != null)
            {
                string folder = "Images/ListingMedia/Banners/";
                addListing.ListingBanner = UploadImage(folder, listingbanner);
            }
            addListing.AddedDate = DateTime.Now;
            _context.AddListings.Add(addListing);
            _context.SaveChanges();
            return Ok(new { status = "success", addListing });

        }
        [HttpPut]
        public async Task<IActionResult> EditListing(IFormFile Listinglogo, IFormFile PromoVideo, IFormFile listingbanner, IFormFileCollection Videos, IFormFileCollection Photos, AddListing addListing)
        {
            var model = _context.AddListings.Where(c => c.AddListingId == addListing.AddListingId).FirstOrDefault();
            if (Listinglogo != null)
            {
                string folder = "Images/ListingMedia/Logos/";
                addListing.ListingLogo = UploadImage(folder, Listinglogo);
                model.ListingLogo = addListing.ListingLogo;
            }
            if (PromoVideo != null)
            {
                string folder = "Images/ListingMedia/Videos/";
                addListing.PromoVideo = UploadImage(folder, PromoVideo);
                model.PromoVideo = addListing.PromoVideo;
            }
            if (listingbanner != null)
            {
                string folder = "Images/ListingMedia/Banners/";
                addListing.ListingBanner = UploadImage(folder, listingbanner);
                model.ListingBanner = addListing.ListingBanner;
            }
            List<ListingPhotos> photolistings = new List<ListingPhotos>();
            if (Photos.Count() > 0)
            {
                for (int i = 0; i < Photos.Count(); i++)
                {
                    ListingPhotos photoobj = new ListingPhotos();
                    if (Photos[i] != null)
                    {
                        string folder = "Images/ListingMedia/Photos/";
                        photoobj.PhotoUrl = UploadImage(folder, Photos[i]);
                    }

                    photolistings.Add(photoobj);
                }
                addListing.ListingPhotos = photolistings;
                model.ListingPhotos = photolistings;
            }


            List<ListingVideos> videoListing = new List<ListingVideos>();
            if (Videos.Count() > 0)
            {
                for (int i = 0; i < Videos.Count(); i++)
                {
                    ListingVideos videoobj = new ListingVideos();
                    if (Videos[i] != null)
                    {
                        string folder = "Images/ListingMedia/Videos/";
                        videoobj.VideoUrl = UploadImage(folder, Videos[i]);
                    }

                    videoListing.Add(videoobj);
                }
                addListing.ListingVideos = videoListing;
                model.ListingVideos = videoListing;

            }
            try
            {
                if (model == null)
                {
                    return Ok(new { status = "false" });

                }
                var branchesid = _context.Branches.Where(a => a.AddListingId == addListing.AddListingId);
                _context.Branches.RemoveRange(branchesid);
                model.Address = addListing.Address;
                model.Title = addListing.Title;
                model.Phone1 = addListing.Phone1;
                model.Phone2 = addListing.Phone2;
                model.Tags = addListing.Tags;
                model.Reviews = addListing.Reviews;
                model.Rating = addListing.Rating;
                model.MainLocataion = addListing.MainLocataion;
                model.CityId = addListing.CityId;
                model.ContactPeroson = addListing.ContactPeroson;
                model.CountryId = addListing.CountryId;
                model.Branches = addListing.Branches;
                model.CategoryId = addListing.CategoryId;
                model.Discription = addListing.Discription;
                model.Email = addListing.Email;
                model.Fax = addListing.Fax;
                model.Website = addListing.Website;

                _context.Attach(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { status = "success", model });

            }
            catch (Exception)
            {
                return Ok(new { status = "failed" });

            }
        }

        [HttpDelete]
        public IActionResult DeleteListing(int ListingId)
        {
            if (ListingId != 0)
            {
                try
                {
                    var Listing = _context.AddListings.Find(ListingId);
                    if (Listing != null)
                    {
                        _context.AddListings.Remove(Listing);
                        _context.SaveChanges();
                        return Ok("Listing Deleted Successfully..");
                    }
                    return BadRequest("Listing Not Found..");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Valid ID..");

        }
        [HttpGet]
        public IActionResult GetListingbyId(int ListingId,string userid)
        {
            if (ListingId != 0)
            {
                try
                {
                    var Listing = _context.AddListings.Include(a => a.Branches).Include(a => a.ListingPhotos).Include(a => a.ListingVideos).Include(a => a.Country).Where(a => a.AddListingId == ListingId).Select(c => new
                    {
                        AddedDate = c.AddedDate,
                        AddListingId = c.AddListingId,
                        Address = c.Address,
                        Branches = c.Branches,
                        CategoryId = c.CategoryId,
                        CountryId = c.CountryId,
                        CityId = c.CityId,
                        Country = c.Country,
                        CreatedByUser = c.CreatedByUser,
                        Discription = c.Discription,
                        ListingLogo = c.ListingLogo,
                        ListingPhotos = c.ListingPhotos,
                        ListingVideos = c.ListingVideos,
                        ListingBanner = c.ListingBanner,
                        Fax = c.Fax,
                        Email = c.Email,
                        Website = c.Website,
                        Phone1 = c.Phone1,
                        Phone2 = c.Phone2,
                        Rating = c.Rating,
                        Reviews = c.Reviews,
                        ContactPeroson = c.ContactPeroson,
                        MainLocataion = c.MainLocataion,
                        PromoVideo = c.PromoVideo,
                        Tags = c.Tags,
                        Title = c.Title,
                        IsFavourite = _context.Favourites.Any(o => o.AddListingId == c.AddListingId && o.UserId == userid),
                        IsFolowing = _context.Folwers.Any(o => o.AddListingId == c.AddListingId && o.UserId == userid),

                    }).FirstOrDefault();

                    if (Listing != null)
                    {
                        return Ok(new { Status = true, Listing = Listing });
                    }
                    return Ok(new { status = false, message = "Listing  NotFound.." });
                    
                }
                catch (Exception e)
                {
                    return Ok(new { status = false, message = e.Message });
                }
            }
            return BadRequest("Enter Valid ID..");
        }
        [HttpGet]
        public IActionResult GetListingbyUserId(string Email,string userId)
        {
            try
            {
                var Listing = _context.AddListings.Include(a => a.Branches).Include(a => a.ListingPhotos).Include(a => a.ListingVideos).Include(a => a.Country).Where(a => a.CreatedByUser == Email).Select(c => new
                {
                    AddedDate = c.AddedDate,
                    AddListingId = c.AddListingId,
                    Address = c.Address,
                    Branches = c.Branches,
                    CategoryId = c.CategoryId,
                    CountryId = c.CountryId,
                    CityId = c.CityId,
                    Country = c.Country,
                    CreatedByUser = c.CreatedByUser,
                    Discription = c.Discription,
                    ListingLogo = c.ListingLogo,
                    ListingPhotos = c.ListingPhotos,
                    ListingVideos = c.ListingVideos,
                    ListingBanner = c.ListingBanner,
                    Fax = c.Fax,
                    Email = c.Email,
                    Website = c.Website,
                    Phone1 = c.Phone1,
                    Phone2 = c.Phone2,
                    Rating = c.Rating,
                    Reviews = c.Reviews,
                    ContactPeroson = c.ContactPeroson,
                    MainLocataion = c.MainLocataion,
                    PromoVideo = c.PromoVideo,
                    Tags = c.Tags,
                    Title = c.Title,
                    IsFavourite = _context.Favourites.Any(o => o.AddListingId == c.AddListingId && o.UserId == userId),
                    IsFolowing = _context.Folwers.Any(o => o.AddListingId == c.AddListingId && o.UserId == userId),

                }).FirstOrDefault();

                if (Listing != null)
                {
                    return Ok(new { Status = true, Listing = Listing });
                }
                return Ok(new { status = false, message = "Listing  NotFound.." });

            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }

        }
        [HttpPost]
        public IActionResult PostListingReview(ReviewVM review)
        {
            if (review.AddListingId != 0)
            {
                var reviewobj = new Review()
                {
                    AddListingId = review.AddListingId,
                    Email = review.Email,
                    Name = review.Name,
                    Rating = review.Rating,
                    Title = review.Title,
                    ReviewDate = DateTime.Now
                };
                try
                {
                    _context.Reviews.Add(reviewobj);
                    _context.SaveChanges();
                    return Ok(new { status = "success", reviewobj });
                }
                catch (Exception e)
                {
                    return Ok(new { status = false, message = e.Message });
                }
            }
            return Ok(new { status = false, message = "Enter Valid AssetListing id.." });
        }
        [HttpGet]
        public IActionResult GetAllReviews(int listingid)
        {
            if (listingid != 0)
            {
                try
                {
                    var reviews = _context.Reviews.Where(a => a.AddListingId == listingid).ToList();
                    if (reviews == null)
                    {
                        return Ok(new { status = "false" });
                    }
                    return Ok(new { status = "success", reviews });
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Enter Valid ListingId..");

        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userEmail, string currentPassword, string newPassword)
        {
            if (currentPassword == newPassword)
            {

                return BadRequest("Current Password Is The Same New Password ");
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return BadRequest("User Not Found ");
                }
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!result.Succeeded)
                {
                    return BadRequest("Can Not Update User Password ");
                }
                await _signInManager.RefreshSignInAsync(user);
                return Ok(new { status = "success" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetClassifiedAdsTypes()
        {
            try
            {
                var ClassifiedTypes = await _context.ClassifiedAdsTypes.ToListAsync();
                var model = new
                {
                    status = true,
                    ClassifiedTypes = ClassifiedTypes
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductStatus()
        {
            try
            {
                var productStatus = await _context.ProductStatuses.ToListAsync();
                var model = new
                {
                    status = true,
                    productStatus = productStatus
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductStatusById(int productStatusId)
        {
            try
            {
                var productStatus = await _context.ProductStatuses.Where(e => e.ProductStatusID == productStatusId).FirstOrDefaultAsync();
                var model = new
                {
                    status = true,
                    StatusTitle = productStatus.StatusTitle
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddClassifiedAds(IFormFile MainPhoto, IFormFileCollection Medias, ClassifiedAds classifiedAds)
        {
            var user = await _userManager.FindByEmailAsync(classifiedAds.AddedBy);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            if (MainPhoto != null)
            {
                string folder = "Images/ClassifiedAdsMedia/Media/";
                classifiedAds.MainPhoto = UploadImage(folder, MainPhoto);
            }

            List<ClassifiedAsdMedia> classifiedAsdMedias = new List<ClassifiedAsdMedia>();
            if (Medias.Count() > 0)
            {
                for (int i = 0; i < Medias.Count(); i++)
                {
                    ClassifiedAsdMedia classifiedAsdMediaObj = new ClassifiedAsdMedia();
                    if (Medias[i] != null)
                    {
                        string folder = "Images/ClassifiedAdsMedia/Media/";
                        classifiedAsdMediaObj.MediaUrl = UploadImage(folder, Medias[i]);
                        classifiedAsdMediaObj.MediaDate = DateTime.Now;
                    }

                    classifiedAsdMedias.Add(classifiedAsdMediaObj);
                }
                classifiedAds.ClassifiedAsdMedias = classifiedAsdMedias;
            }

            classifiedAds.Status = false;
            classifiedAds.AddedDate = DateTime.Now;
            classifiedAds.PayedDate = null;


            _context.ClassifiedAds.Add(classifiedAds);
            _context.SaveChanges();
            return Ok(new { status = "success", classifiedAds });


        }
        [HttpPut]
        public async Task<IActionResult> EditClassifiedAds(IFormFile MainPhoto, IFormFileCollection Medias, ClassifiedAdsVm classifiedAds)
        {
            var model = await _context.ClassifiedAds.Where(c => c.ClassifiedAdsID == classifiedAds.ClassifiedAdsID).FirstOrDefaultAsync();
            if (model == null)
            {
                return Ok(new { status = false, message = "Classified Ads Object Not Found" });
            }
            if (MainPhoto != null)
            {
                string folder = "Images/ClassifiedAdsMedia/Media/";
                model.MainPhoto = UploadImage(folder, MainPhoto);
            }

            List<ClassifiedAsdMedia> classifiedAsdMedias = new List<ClassifiedAsdMedia>();
            if (Medias.Count() > 0)
            {
                for (int i = 0; i < Medias.Count(); i++)
                {
                    ClassifiedAsdMedia classifiedAsdMediaObj = new ClassifiedAsdMedia();
                    if (Medias[i] != null)
                    {
                        string folder = "Images/ClassifiedAdsMedia/Media/";
                        classifiedAsdMediaObj.MediaUrl = UploadImage(folder, Medias[i]);
                        classifiedAsdMediaObj.MediaDate = DateTime.Now;
                        classifiedAsdMediaObj.ClassifiedAdsID = model.ClassifiedAdsID;
                    }

                    classifiedAsdMedias.Add(classifiedAsdMediaObj);
                }
                _context.ClassifiedAsdMedias.AddRange(classifiedAsdMedias);
            }

            model.ClassifiedAdsLocation = classifiedAds.ClassifiedAdsLocation;
            model.Title = classifiedAds.Title;
            model.Details = classifiedAds.Details;
            model.Price = classifiedAds.Price;
            model.ProductStatusID = classifiedAds.ProductStatusID;
            model.ClassifiedAdsTypeID = classifiedAds.ClassifiedAdsTypeID;
            model.Status = classifiedAds.Status;
            model.AddedDate = DateTime.Now;
            _context.Attach(model).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(new { status = true, ClassifiedAds = model });
        }
        [HttpDelete]
        public IActionResult DeleteClassifiedAds(int classifiedAdsId)
        {
            if (classifiedAdsId != 0)
            {
                try
                {
                    var classifiedAds = _context.ClassifiedAds.Find(classifiedAdsId);
                    if (classifiedAds != null)
                    {
                        _context.ClassifiedAds.Remove(classifiedAds);
                        _context.SaveChanges();
                        return Ok(new { status = "Deleted", classifiedAds });
                    }
                    return Ok(new { status = false, message = "classified Ads Not Found.." });

                }
                catch (Exception e)
                {
                    return Ok(new { status = false, message = e.Message });
                }
            }
            return Ok(new { status = false, message = "Enter Valid Classified Ads ID.." });


        }
        [HttpDelete]
        public IActionResult DeleteClassifiedAdsMedia(int MediaId)
        {
            if (MediaId != 0)
            {
                try
                {
                    var classifiedAdsMedia = _context.ClassifiedAsdMedias.Find(MediaId);
                    if (classifiedAdsMedia != null)
                    {
                        _context.ClassifiedAsdMedias.Remove(classifiedAdsMedia);
                        _context.SaveChanges();
                        return Ok(new { status = "Deleted", classifiedAdsMedia });
                    }
                    return Ok(new { status = false, message = "classified Ads Media Not Found.." });
                }
                catch (Exception e)
                {
                    return Ok(new { status = false, message = e.Message });
                }
            }
            return Ok(new { status = false, message = "Enter Valid Classified Ads Media ID.." });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassifiedAdsByTypeId(int ClassifiedAdsTypeId)
        {
            try
            {
                var classifiedAds = await _context.ClassifiedAds.Include(e => e.ClassifiedAsdMedias).Where(e => e.ClassifiedAdsTypeID == ClassifiedAdsTypeId).ToListAsync();
                var model = new
                {
                    status = true,
                    classifiedAds = classifiedAds
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassifiedAdsByType_Status(int ClassifiedAdsTypeId, int productStatustId)
        {
            try
            {
                var classifiedAds = await _context.ClassifiedAds.Include(e => e.ClassifiedAsdMedias).Where(e => e.ClassifiedAdsTypeID == ClassifiedAdsTypeId && e.ProductStatusID == productStatustId).ToListAsync();
                var model = new
                {
                    status = true,
                    classifiedAds = classifiedAds
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassifiedAdsByUser(string UserEmail,string userid)
        {
            try
            {
                var classifiedAds = _context.ClassifiedAds.Include(e => e.ClassifiedAsdMedias).Include(e => e.ClassifiedAdsType).Include(e => e.Quotations).Where(e => e.AddedBy == UserEmail).Select(e => new
                {
                    AddedDate = e.AddedDate,
                    AddedBy = e.AddedBy,
                    ClassifiedAdsID = e.ClassifiedAdsID,
                    Title = e.Title,
                    Details = e.Details,
                    ClassifiedAdsLocation = e.ClassifiedAdsLocation,
                    ClassifiedAdsType = e.ClassifiedAdsType,
                    ClassifiedAsdMedias = e.ClassifiedAsdMedias,
                    Quotations = e.Quotations,
                    MainPhoto = e.MainPhoto,
                    ProductStatus = e.ProductStatus,
                    Price = e.Price,
                    PayedDate = e.PayedDate,
                    IsFavourite = _context.FavouriteClassifieds.Any(o => o.ClassifiedAdsID == e.ClassifiedAdsID && o.UserId == userid),
                    IsFolowing = _context.FolowClassifieds.Any(o => o.ClassifiedAdsID == e.ClassifiedAdsID && o.UserId == userid),


                }).ToList();
               
                var classifiedAdsMedia = await _context.ClassifiedAsdMedias.Where(e => e.ClassifiedAds.AddedBy == UserEmail).OrderBy(e => e.MediaDate).ToListAsync();

                var model = new
                {
                    status = true,
                    classifiedAds = classifiedAds

                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassifiedAdsTypes()
        {
            try
            {
                var classifiedAdsTypes = await _context.ClassifiedAdsTypes.ToListAsync();
                var model = new
                {
                    status = true,
                    classifiedAdsTypes = classifiedAdsTypes
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetClassifiedAdsTypeByTypeId(int typeId)
        {
            try
            {
                var classifiedAdsType = await _context.ClassifiedAdsTypes.Where(e => e.ClassifiedAdsTypeID == typeId).FirstOrDefaultAsync();
                var model = new
                {
                    status = true,
                    classifiedAdsType = classifiedAdsType
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var countries = await _context.Countries.ToListAsync();
                var model = new
                {
                    status = true,
                    Countries = countries
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ValidateBranchModel(BranchVM branch)
        {
            if (branch.Lat == null || branch.Long == null || branch.Title == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public IActionResult AddBranch(BranchVM branch)
        {
            var lsting = _context.AddListings.Find(branch.AddListingId);
            if (lsting == null)
            {
                return Ok(new { status = false, message = "AddListing Not Found..." });
            }
            if (!ValidateBranchModel(branch))
            {
                return Ok(new { status = false, message = "Enter Required Data..." });
            }
            try
            {
                var model = new Branch() { AddListingId = branch.AddListingId, Lat = branch.Lat, Long = branch.Long, Title = branch.Title };
                _context.Branches.Add(model);
                _context.SaveChanges();
                return Ok(new { status = true, Branch = model });

            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPut]
        public IActionResult EditBranch(BranchVM branch)
        {

            if (branch.BranchId == 0)
            {
                return Ok(new { status = false, message = "Enter BranchId..." });
            }
            var model = _context.Branches.Find(branch.BranchId);
            if (model == null)
            {
                return Ok(new { status = false, message = "Branch Not Found..." });
            }
            try
            {
                model.Title = branch.Title;
                model.Lat = branch.Lat;
                model.Long = branch.Long;
                _context.Attach(model).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new { status = true, Branch = model });
            }
            catch (Exception e)
            {

                return Ok(new { status = false, message = e.Message });

            }
        }
        [HttpDelete]
        public IActionResult DeleteBranch(int BranchId)
        {
            var model = _context.Branches.Find(BranchId);
            if (model == null)
            {
                return Ok(new { status = false, message = "Branch Not Found.." });
            }
            try
            {
                _context.Branches.Remove(model);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Branch Deleted Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetBranchById(int branchid)
        {

            try
            {
                var Branch = _context.Branches.Find(branchid);
                var model = new
                {
                    status = true,
                    Branch = Branch
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            try
            {
                var Branches = await _context.Branches.ToListAsync();
                var model = new
                {
                    status = true,
                    Branches = Branches
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetBuisnessMedia(int buisnessid)
        {
            try
            {
                var listing = _context.AddListings.Find(buisnessid);
                if (listing == null)
                {
                    return Ok(new { Status = "false", Reason = "Buisness Not Found" });
                }
                var media = new List<MediaVM>();

                var photosList = _context.ListingPhotos.Where(e => e.AddListingId == buisnessid);
                var VideosList = _context.ListingVideos.Where(e => e.AddListingId == buisnessid);
                if (photosList != null)
                {
                    foreach (var item in photosList)
                    {
                        var mediaobj = new MediaVM() { Caption = item.Caption, MediaURL = item.PhotoUrl, PublishDate = item.PublishDate };
                        media.Add(mediaobj);
                    }
                }
                if (VideosList != null)
                {
                    foreach (var item in VideosList)
                    {
                        var mediaobj = new MediaVM() { Caption = item.Caption, MediaURL = item.VideoUrl, PublishDate = item.PublishDate };
                        media.Add(mediaobj);
                    }
                }
                if (media != null)
                {
                    media = media.OrderBy(a => a.PublishDate).ToList();
                }

                return Ok(new { Status = "Success", Media = media });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Reason = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllBuisness(string userId)
        {
            
            try
            {
                var Buisness = _context.AddListings.Include(a => a.Branches).Include(a => a.ListingPhotos).Include(a => a.ListingVideos).Include(a => a.Country).Select(c => new
                {
                    AddedDate = c.AddedDate,
                    AddListingId = c.AddListingId,
                    Address = c.Address,
                    Branches = c.Branches,
                    CategoryId = c.CategoryId,
                    CountryId = c.CountryId,
                    CityId = c.CityId,
                    Country = c.Country,
                    CreatedByUser = c.CreatedByUser,
                    Discription = c.Discription,
                    ListingLogo = c.ListingLogo,
                    ListingPhotos = c.ListingPhotos,
                    ListingVideos = c.ListingVideos,
                    ListingBanner = c.ListingBanner,
                    Fax = c.Fax,
                    Email = c.Email,
                    Website = c.Website,
                    Phone1 = c.Phone1,
                    Phone2 = c.Phone2,
                    Rating = c.Rating,
                    Reviews = c.Reviews,
                    ContactPeroson = c.ContactPeroson,
                    MainLocataion = c.MainLocataion,
                    PromoVideo = c.PromoVideo,
                    Tags = c.Tags,
                    Title = c.Title,
                    IsFavourite = _context.Favourites.Any(o => o.AddListingId == c.AddListingId && o.UserId == userId),
                    IsFolowing = _context.Folwers.Any(o => o.AddListingId == c.AddListingId && o.UserId == userId),

                }).ToList();

                if (Buisness != null)
                {
                    return Ok(new { Status = true, Buisness = Buisness });
                }
                return Ok(new { status = false, message = "Buisness  NotFound.." });

            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult AddFavouriteBuisness(int buisnessid, string userid)
        {
            try
            {

                var buisness = _context.AddListings.Find(buisnessid);
                var user = _applicationDbContext.Users.Find(userid);
                if (buisness == null)
                {
                    return Ok(new { status = false, message = "Buisness Not Found." });

                }
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });

                }
                var favourite = _context.Favourites.Where(a => a.AddListingId == buisnessid && a.UserId == userid).FirstOrDefault();
                if (favourite != null)
                {
                    return Ok(new { status = false, message = "Buisness already added in favourite.." });
                }
                var favouriteobj = new Favourite() { UserId = userid, AddListingId = buisnessid };
                _context.Favourites.Add(favouriteobj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Buisness Added To Favourite.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteFavouriteBuisness(int buisnessId, string userid)
        {
            try
            {
                var favourite = _context.Favourites.Where(e=>e.UserId==userid&&e.AddListingId== buisnessId).FirstOrDefault();
                if (favourite == null)
                {
                    return Ok(new { status = false, message = "Favourite Buisness Not Found.." });
                }
                _context.Favourites.Remove(favourite);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Buisness Deleted Successfully From Favourite.." });

            }
            catch (Exception e)
            {

                return Ok(new { status = false, message = e.Message });

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFavouriteBuisnessByUser(string userid)
        {
            try
            {
                var user = _applicationDbContext.Users.Find(userid);
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });
                }
                var Favourite = await _context.Favourites.Where(a => a.UserId == userid).Include(a => a.AddListing).ToListAsync();
                var model = new
                {
                    status = true,
                    FavouriteBusness = Favourite
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetFavouriteBuisnessCountByUser(string userid)
        {
            try
            {
                var user = _applicationDbContext.Users.Find(userid);

                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });
                }
                var Favourite = _context.Favourites.Where(a => a.UserId == userid).Count();
                var model = new
                {
                    status = true,
                    FavouriteCount = Favourite
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassifiedAdsMediaByUser(string UserEmail)
        {
            try
            {

                var classifiedAdsMedia = await _context.ClassifiedAsdMedias.Include(e => e.ClassifiedAds).Where(e => e.ClassifiedAds.AddedBy == UserEmail).OrderBy(e => e.MediaDate).ToListAsync();
                var model = new
                {
                    status = true,
                    classifiedAdsMedia = classifiedAdsMedia
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetCountClassifiedAdsByUser(string UserEmail)
        {
            try
            {

                int classifiedAdsCount = _context.ClassifiedAds.Where(e => e.AddedBy == UserEmail).Count();
                var model = new
                {
                    status = true,
                    Count = classifiedAdsCount
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetCountAddListingByUser(string UserEmail)
        {
            try
            {

                int addListingCount = _context.AddListings.Where(e => e.CreatedByUser == UserEmail).Count();
                var model = new
                {
                    status = true,
                    Count = addListingCount
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAddListingByCountry(int CountryId)
        {
            try
            {
                var rnd = new Random();
                var addListingList = _context.AddListings.Where(e => e.CountryId == CountryId).ToList();
                var RandomaddListingList = addListingList.OrderBy(x => rnd.Next());
                var model = new
                {
                    status = true,
                    AddListingList = RandomaddListingList
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetSearchResult(string Search)
        {
            if (Search == null)
            {
                return Ok(new { status = false, message = "Enter Any Text To search." });
            }
            try
            {
                var users = _applicationDbContext.Users.Where(a => a.UserName.ToUpper().Contains(Search.ToUpper())).ToList();
                var buisness = _context.AddListings.Include(a => a.Country).Include(a => a.Category).Include(a => a.Reviews).Include(a => a.Branches).Include(a => a.ListingPhotos).Include(a => a.ListingVideos).Where(a => a.Country.CountryTlEn.ToUpper().Contains(Search.ToUpper()) || a.Category.CategoryTitleEn.ToUpper().Contains(Search.ToUpper()) || a.Category.CategoryTitleAr.ToUpper().Contains(Search.ToUpper())).ToList();
                var classifiedads = _context.ClassifiedAds.Include(a => a.ClassifiedAdsType).Where(a => a.ClassifiedAdsType.TypeTitleEn.ToUpper().Contains(Search.ToUpper()) || a.ClassifiedAdsType.TypeTitleAr.ToUpper().Contains(Search.ToUpper()) || a.Title.ToUpper().Contains(Search.ToUpper())).ToList();
                //var userssw = await _userManager.FindByEmailAsync(Search);
                var Searchresult = new List<SearchVM>();
                foreach (var item in users)
                {
                    var userssw = await _userManager.FindByEmailAsync(item.UserName);
                    var searchobj = new SearchVM() { Email = item.Email,id = item.Id, Title = userssw.FullName, CategoryEn = userssw.Job, CategoryAr = userssw.Job, image = userssw.ProfilePicture, Type = 1 };
                    Searchresult.Add(searchobj);
                }
                foreach (var item in buisness)
                {
                    var searchobj = new SearchVM() { Email = item.CreatedByUser, id = item.AddListingId.ToString(), CategoryEn = item.Category.CategoryTitleEn, CategoryAr = item.Category.CategoryTitleAr, Title = item.Title, image = item.ListingLogo, Type = 2 };
                    Searchresult.Add(searchobj);
                }
                foreach (var item in classifiedads)
                {
                    var searchobj = new SearchVM() { Email = item.AddedBy, id = item.ClassifiedAdsID.ToString(), CategoryEn = item.ClassifiedAdsType.TypeTitleEn, CategoryAr = item.ClassifiedAdsType.TypeTitleAr, Title = item.Title, image = item.MainPhoto, Type = 3 };
                    Searchresult.Add(searchobj);
                }
                if (Searchresult.Count() == 0)
                {
                    return Ok(new { status = false, message = "Not Matched Result.. " });
                }
                var rnd = new Random();
                var RandomList = Searchresult.OrderBy(x => rnd.Next());

                var model = new
                {
                    status = true,
                    searchresult = RandomList
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult AddQuotation(QuotationVM quotationVM)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == quotationVM.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var classifiedAdsObj = _context.ClassifiedAds.Where(e => e.ClassifiedAdsID == quotationVM.ClassifiedAdsID).FirstOrDefault();
                if (classifiedAdsObj == null)
                {
                    return Ok(new { status = false, message = "Classified Not Found" });
                }
                var quotation = new Quotation()
                {
                    ClassifiedAdsID = quotationVM.ClassifiedAdsID,
                    UserId = quotationVM.UserId,
                    QuotationDate = DateTime.Now,
                    Description = quotationVM.Description,
                };

                _context.Quotations.Add(quotation);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Quotation Added Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult FlowerToBussiness(FolowVm folwers)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var addListingObj = _context.AddListings.Where(e => e.AddListingId == folwers.AddListingId).FirstOrDefault();
                if (addListingObj == null)
                {
                    return Ok(new { status = false, message = "Bussiness Not Found" });
                }
                var folwerObj = new Folwers()
                {
                    UserId = folwers.UserId,
                    AddListingId = folwers.AddListingId

                };

                _context.Folwers.Add(folwerObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Folwer Added Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult UnFlowerToBussiness(FolowVm folwers)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var addListingObj = _context.AddListings.Where(e => e.AddListingId == folwers.AddListingId).FirstOrDefault();
                if (addListingObj == null)
                {
                    return Ok(new { status = false, message = "Bussiness Not Found" });
                }
                var folwerObj = _context.Folwers.Where(e => e.AddListingId == folwers.AddListingId && e.UserId == folwers.UserId).FirstOrDefault();

                _context.Folwers.Remove(folwerObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Folwer Deleted Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetBussinessFolwerCount(int bussinessId)
        {

            try
            {

                int FolwersCount = _context.Folwers.Where(e => e.AddListingId == bussinessId).Count();
                var model = new
                {
                    Status = true,
                    Count = FolwersCount
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }

        }
        [HttpGet]
        public IActionResult GetAllQuotation()
        {
            try
            {

                var Quotations = _context.Quotations.ToList();
                var model = new
                {
                    status = true,
                    QuotationsList = Quotations
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetQuotationByUserId(string userId)
        {

            try
            {

                var Quotation = _context.Quotations.Where(e => e.UserId == userId).ToList();
                var model = new
                {
                    Status = true,
                    QuotationList = Quotation
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }

        }
        [HttpGet]
        public IActionResult GetAllCitesByCountry(int countryId)
        {

            try
            {

                var Cites = _context.Cities.Where(e => e.CountryId == countryId).ToList();
                var model = new
                {
                    Status = true,
                    CitesList = Cites
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }

        }
        [HttpPost]
        public IActionResult AddMessage(Contact contact)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Email == contact.Email).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                if (contact.Message == null)
                {
                    return Ok(new { status = false, message = "Message Is Required" });

                }
                if (contact.FullName == null)
                {
                    return Ok(new { status = false, message = "FullName Is Required" });

                }
                contact.SendingDate = DateTime.Now;
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Message Send Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetPagesContentById([FromQuery] int PageContentId)
        {

            try
            {

                var pageContent = _context.PageContents.FirstOrDefault(c => c.PageContentId == PageContentId);
                if (pageContent == null)
                {
                    return Ok(new { status = false, message = "Object Not Found" });

                }
                return Ok(new { status = true, pageContent = pageContent });

            }
            catch (Exception)
            {

                return Ok(new { status = false, message = "Something went wrong" });

            }
        }
        [HttpGet]
        public IActionResult getAllSocialLinks()
        {
            try
            {
                string adminRoleId = _applicationDbContext.Roles.Where(e => e.Name == "Admin").FirstOrDefault().Id;
                var UserAdminId = _applicationDbContext.UserRoles.Where(e => e.RoleId == adminRoleId).FirstOrDefault().UserId;
                var user = _userManager.Users.Where(e => e.Id == UserAdminId).FirstOrDefault();
                var SocialLinks = _context.SoicialMidiaLinks.ToList().Take(1).FirstOrDefault();

                if (SocialLinks == null)
                {
                    return Ok(new { Success = false, message = "Object Not Exist" });
                }
                var SocialObj = new SocialLinksVm()
                {
                    Instgramlink = SocialLinks.Instgramlink,
                    WhatsApplink = SocialLinks.WhatsApplink,
                    YoutubeLink = SocialLinks.WhatsApplink,
                    TwitterLink = SocialLinks.TwitterLink,
                    LinkedInlink = SocialLinks.LinkedInlink,
                    facebooklink = SocialLinks.facebooklink,
                    id = SocialLinks.id,
                    AdminEmail = user.Email,
                    AdminPhone = user.PhoneNumber

                };

                return Ok(new { Success = true, SocialLinks = SocialObj });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });

            }


        }
        [HttpDelete]
        public IActionResult DeleteBussinessPhoto(int ListingPhotoId)
        {

            try
            {

                var ListinPhotogObj = _context.ListingPhotos.Where(e => e.Id == ListingPhotoId).FirstOrDefault();
                if (ListinPhotogObj == null)
                {
                    return Ok(new { status = false, message = "Bussiness Photo Object Not Found" });
                }

                _context.ListingPhotos.Remove(ListinPhotogObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Photo Deleted Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }

        }
        [HttpDelete]
        public IActionResult DeleteBussinessVedio(int ListingVedioId)
        {

            try
            {

                var ListingVideoObj = _context.ListingVideos.Where(e => e.Id == ListingVedioId).FirstOrDefault();
                if (ListingVideoObj == null)
                {
                    return Ok(new { status = false, message = "Bussiness Video Object Not Found" });
                }

                _context.ListingVideos.Remove(ListingVideoObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Video Deleted Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }

        }
        [HttpGet]
        public IActionResult GetAllMediaForBussiness(int bussinessId)
        {
            try
            {
                var addListingsObj = _context.AddListings.Where(e=>e.AddListingId==bussinessId).FirstOrDefault();
                if (addListingsObj == null)
                {
                    return Ok(new { Status = "false", Message = "Bussiness Not Found" });
                }
                var media = new List<MediaVM>();

                var photosList = _context.ListingPhotos.Where(e => e.AddListingId == bussinessId);
                var VideosList = _context.ListingVideos.Where(e => e.AddListingId == bussinessId);
                if (photosList != null)
                {
                    foreach (var item in photosList)
                    {
                        var mediaobj = new MediaVM() { MediaId = item.Id, Caption = item.Caption, MediaURL = item.PhotoUrl, PublishDate = item.PublishDate };
                        media.Add(mediaobj);
                    }
                }
                if (VideosList != null)
                {
                    foreach (var item in VideosList)
                    {
                        var mediaobj = new MediaVM() { MediaId = item.Id, Caption = item.Caption, MediaURL = item.VideoUrl, PublishDate = item.PublishDate };
                        media.Add(mediaobj);
                    }
                }
                if (media != null)
                {
                    media = media.OrderBy(a => a.PublishDate).ToList();
                }

                return Ok(new { Status = "Success", Media = media });

            }
            catch (Exception e)
            {
                return Ok(new { Status = "false", Message = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetFAQList()
        {
            try
            {
                var faqsList = _context.FAQ.ToList();
                return Ok(new { Status = true, FaqsList = faqsList });

            }
            catch (Exception ex)
            {

                return Ok(new { Status = false, message = ex.Message });

            }

        }
        [HttpGet]
        public IActionResult GetClassifiedAdsById(int classifiedAdsId,string userid)
        {
            try
            {
                var classifiedAdsObj = _context.ClassifiedAds.Include(a => a.ClassifiedAdsType).Include(a => a.ProductStatus).Include(a => a.ClassifiedAsdMedias).Include(a=>a.Quotations).Where(a => a.ClassifiedAdsID == classifiedAdsId).Select(c => new
                {
                    ClassifiedAdsID = c.ClassifiedAdsID,
                    AddedBy = c.AddedBy,
                    AddedDate = c.AddedDate,
                    Details = c.Details,
                    ClassifiedAdsLocation = c.ClassifiedAdsLocation,
                    ProductStatusID = c.ProductStatusID,
                    ClassifiedAdsTypeID = c.ClassifiedAdsTypeID,
                    MainPhoto = c.MainPhoto,
                    PayedDate = c.PayedDate,
                    Price = c.Price,
                    Title = c.Title,
                    Quotations = c.Quotations,
                    ClassifiedAsdMedias = c.ClassifiedAsdMedias,
                    Status = c.Status,
                    IsFavourite = _context.FavouriteClassifieds.Any(o => o.ClassifiedAdsID == c.ClassifiedAdsID && o.UserId == userid),
                    IsFolowing = _context.FolowClassifieds.Any(o => o.ClassifiedAdsID == c.ClassifiedAdsID && o.UserId == userid),

                }).FirstOrDefault();

                //var classifiedAdsObj = _context.ClassifiedAds.Where(e => e.ClassifiedAdsID == classifiedAdsId).Include(e=>e.Quotations).Include(e => e.ClassifiedAsdMedias).Include(e => e.ProductStatus).FirstOrDefault();
                if (classifiedAdsObj == null)
                {
                    return Ok(new { Status = false, message = "Classified Ads Not Found" });
                }
               
                    return Ok(new { Status = true, ClassifiedAds = classifiedAdsObj });
            }
            catch (Exception ex)
            {

                return Ok(new { Status = false, message = ex.Message });

            }

        }
        [HttpPost]
        public IActionResult AddFavouriteClassifiedAds(int classifiedId, string userid)
        {
            try
            {

                var buisness = _context.ClassifiedAds.Find(classifiedId);
                var user = _applicationDbContext.Users.Find(userid);
                if (buisness == null)
                {
                    return Ok(new { status = false, message = "classified Not Found." });

                }
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });

                }
                var favourite = _context.FavouriteClassifieds.Where(a => a.ClassifiedAdsID == classifiedId && a.UserId == userid).FirstOrDefault();
                if (favourite != null)
                {
                    return Ok(new { status = false, message = "classified already added in favourite.." });
                }
                var favouriteobj = new FavouriteClassified() { UserId = userid, ClassifiedAdsID = classifiedId };
                _context.FavouriteClassifieds.Add(favouriteobj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Classified Added To Favourite.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult AddFavouriteToProfile(string userProfileId, string userid)
        {
            try
            {

                var userProfile = _applicationDbContext.Users.Where(e=>e.Id== userProfileId).FirstOrDefault();
                var user = _applicationDbContext.Users.Find(userid);
                if (userProfile == null)
                {
                    return Ok(new { status = false, message = "User Profile Not Found." });

                }
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });

                }
                var favourite = _context.FavouriteProfiles.Where(a => a.Id == userProfileId && a.UserId == userid).FirstOrDefault();
                if (favourite != null)
                {
                    return Ok(new { status = false, message = "User Profile already added in favourite.." });
                }
                var favouriteobj = new FavouriteProfile() { UserId = userid, Id = userProfileId };
                _context.FavouriteProfiles.Add(favouriteobj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "User Profile Added To Favourite.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFavouriteClassifiedByUser(string userid)
        {
            try
            {
                var user = _applicationDbContext.Users.Find(userid);
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });
                }
                var Favourite = await _context.FavouriteClassifieds.Where(a => a.UserId == userid).Include(a => a.ClassifiedAds).ToListAsync();
                var model = new
                {
                    status = true,
                    FavouriteClassified = Favourite
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFavouriteProfileByUser(string userid)
        {
            try
            {
                var user = _applicationDbContext.Users.Find(userid);
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found." });
                }
                var Favourite = await _context.FavouriteProfiles.Where(a => a.UserId == userid).ToListAsync();
                var model = new
                {
                    status = true,
                    FavouriteProfiles = Favourite
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult FlowerToClassifiedAds(FollowClassifiedVm folwers)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var addListingObj = _context.ClassifiedAds.Where(e => e.ClassifiedAdsID == folwers.ClssifiedId).FirstOrDefault();
                if (addListingObj == null)
                {
                    return Ok(new { status = false, message = "Classified Not Found" });
                }
                var folwerObj = new FolowClassified()
                {
                    UserId = folwers.UserId,
                    ClassifiedAdsID = folwers.ClssifiedId

                };

                _context.FolowClassifieds.Add(folwerObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Folwer To Classified Added Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult FlowerToProfile(FolowProfileVm folwers)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var userProfile = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (userProfile == null)
                {
                    return Ok(new { status = false, message = "User Profile Not Found" });
                }
                var folwerObj = new FolowProfile()
                {
                    UserId = folwers.UserId,
                     Id= folwers.ProfileId

                };

                _context.FolowProfile.Add(folwerObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Folwer To Profile Added Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult UnFlowerToClassified(FollowClassifiedVm folwers)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var classifiedAdsObj = _context.ClassifiedAds.Where(e => e.ClassifiedAdsID == folwers.ClssifiedId).FirstOrDefault();
                if (classifiedAdsObj == null)
                {
                    return Ok(new { status = false, message = "Classified Not Found" });
                }
                var folwerObj = _context.FolowClassifieds.Where(e => e.ClassifiedAdsID == folwers.ClssifiedId && e.UserId == folwers.UserId).FirstOrDefault();

                _context.FolowClassifieds.Remove(folwerObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Folwer To Classified Deleted Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult UnFlowerToProfile(FolowProfileVm folwers)
        {

            try
            {
                var user = _applicationDbContext.Users.Where(a => a.Id == folwers.UserId).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new { status = false, message = "User Not Found" });
                }
                var classifiedAdsObj = _applicationDbContext.Users.Where(e => e.Id == folwers.ProfileId).FirstOrDefault();
                if (classifiedAdsObj == null)
                {
                    return Ok(new { status = false, message = "Profile Not Found" });
                }
                var folwerObj = _context.FolowProfile.Where(e => e.Id == folwers.ProfileId && e.UserId == folwers.UserId).FirstOrDefault();

                _context.FolowProfile.Remove(folwerObj);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Folwer To Profile Deleted Successfully.." });
            }
            catch (Exception e)
            {
                return Ok(new { status = false, message = e.Message });
            }
        }
        [HttpGet]
        public IActionResult GetClassifiedAdsFolwerCount(int classifiedId)
        {

            try
            {

                int FolwersCount = _context.FolowClassifieds.Where(e => e.ClassifiedAdsID == classifiedId).Count();
                var model = new
                {
                    Status = true,
                    Count = FolwersCount
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }

        }
        [HttpGet]
        public IActionResult GetProfileFolwerCount(string profileId)
        {

            try
            {

                int FolwersCount = _context.FolowProfile.Where(e => e.Id == profileId).Count();
                var model = new
                {
                    Status = true,
                    Count = FolwersCount
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(new { status = true, Reason = e.Message });
            }

        }
        [HttpDelete]
        public IActionResult DeleteFavouriteClassified(int classifiedId, string userid)
        {
            try
            {
                var favourite = _context.FavouriteClassifieds.Where(e=>e.ClassifiedAdsID==classifiedId&&e.UserId==userid).FirstOrDefault();
                if (favourite == null)
                {
                    return Ok(new { status = false, message = "Favourite Classified Not Found.." });
                }
                _context.FavouriteClassifieds.Remove(favourite);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Classified Deleted Successfully From Favourite.." });

            }
            catch (Exception e)
            {

                return Ok(new { status = false, message = e.Message });

            }
        }
        [HttpDelete]
        public IActionResult DeleteFavouriteProfile(string profileId, string userid)
        {
            try
            {
                var favourite = _context.FavouriteProfiles.Where(e => e.Id == profileId && e.UserId == userid).FirstOrDefault();
                if (favourite == null)
                {
                    return Ok(new { status = false, message = "Favourite Profile Not Found.." });
                }
                _context.FavouriteProfiles.Remove(favourite);
                _context.SaveChanges();
                return Ok(new { status = true, message = "Profile Deleted Successfully From Favourite.." });

            }
            catch (Exception e)
            {

                return Ok(new { status = false, message = e.Message });

            }
        }
    }

}

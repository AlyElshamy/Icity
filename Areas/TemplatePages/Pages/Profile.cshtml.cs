using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Icity.Data;
using Icity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace Icity.Areas.TemplatePages
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public ApplicationUser userProfile { get; set; }
        public IcityContext _context { get; set; }
        public IToastNotification ToastNotification { get; }
        public ApplicationDbContext _applicationDbContext { get; set; }
        public static List<Skill> Skills;
        public List<Skill> SkillsList = new List<Skill>();
        public static List<Interest> Interests;
        public List<Interest> InterestsList = new List<Interest>();
        public static List<Language> Languages;
        public List<Language> LanguagesList = new List<Language>();
        public Education education { get; set; }
        public static List<Education> Educations;
        public List<Education> EducationsList = new List<Education>();
        public LifeEvent LifeEvent { get; set; }
        public static ApplicationUser user { set; get; }
        public static List<LifeEvent> LifeEvents;
        public List<LifeEvent> LifeEventsList = new List<LifeEvent>();
        public ProfileModel(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IcityContext Context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            ToastNotification = toastNotification;
            _context = Context;
            _applicationDbContext = applicationDbContext;
            userProfile = new ApplicationUser();

        }
        public IActionResult OnPostFillSkillsList([FromBody] List<string> SkillsTitle)
        {
            var Skillsid = _applicationDbContext.Skills.Where(a => a.Id == user.Id);
            _applicationDbContext.Skills.RemoveRange(Skillsid);
            _applicationDbContext.SaveChanges();
            Skills = new List<Skill>();
            foreach (var item in SkillsTitle)
            {
                Skill Skill = new Skill { SkillTitle = item };
                Skills.Add(Skill);
            }
            ToastNotification.AddSuccessToastMessage("Skill..");

            return new JsonResult(Skills);
        }
        public IActionResult OnPostFillInterestsList([FromBody] List<string> InterestsTitle)
        {
            var Interristid = _applicationDbContext.Interests.Where(a => a.Id == user.Id);
            _applicationDbContext.Interests.RemoveRange(Interristid);
            _applicationDbContext.SaveChanges();
            Interests = new List<Interest>();
            foreach (var item in InterestsTitle)
            {
                Interest Interrist = new Interest { InterestTitle = item};
                Interests.Add(Interrist);
            }
            return new JsonResult(Interests);
        }

        public IActionResult OnPostFillLanguagesList([FromBody] List<string> LanguagesTitle)
        {
            var Languagesid = _applicationDbContext.Languages.Where(a => a.Id == user.Id);
            _applicationDbContext.Languages.RemoveRange(Languagesid);
            _applicationDbContext.SaveChanges();

            Languages = new List<Language>();
            foreach (var item in LanguagesTitle)
            {
                Language language = new Language { LanguageTitle = item};
                Languages.Add(language);
            }
            return new JsonResult(Languages);
        }

        public IActionResult OnPostFillEducationsList([FromBody] List<Education> Educationslist)
        {
            var Educationsid = _applicationDbContext.Educations.Where(a => a.Id == user.Id);
            _applicationDbContext.Educations.RemoveRange(Educationsid);
            _applicationDbContext.SaveChanges();
            Educations = Educationslist;
            return new JsonResult(Educations);
        }
        public IActionResult OnPostFillLifeEventsList([FromBody] List<LifeEvent> lifeEventsList)
        {
            LifeEvents = lifeEventsList;
            return new JsonResult(LifeEventsList);
        }
        public IActionResult OnPostDeletePhotoById([FromBody] int PhotoId)
        {
            var photoobj = _context.ListingPhotos.Where(a => a.Id == PhotoId).FirstOrDefault();
            _context.ListingPhotos.Remove(photoobj);
            _context.SaveChanges();
            return Page();
        }
        public IActionResult OnPostDeleteVideoById([FromBody] int Videoid)
        {
            var Videoobj = _context.ListingVideos.Where(a => a.Id == Videoid).FirstOrDefault();
            _context.ListingVideos.Remove(Videoobj);
            _context.SaveChanges();
            return Page();



        }
        public async Task<IActionResult> OnGet()
        {
            user = await _userManager.GetUserAsync(User);
            SkillsList=Skills = _applicationDbContext.Skills.Where(a => a.Id == user.Id).ToList();
            InterestsList =Interests= _applicationDbContext.Interests.Where(a => a.Id == user.Id).ToList();
            LanguagesList=Languages = _applicationDbContext.Languages.Where(a => a.Id == user.Id).ToList();
            EducationsList=Educations = _applicationDbContext.Educations.Where(a => a.Id == user.Id).ToList();
            LifeEvents =LifeEvents= _applicationDbContext.LifeEvents.Where(a => a.Id == user.Id).ToList();
            
            return Page();
        }


        public async Task<IActionResult> OnPost(IFormFile Profilepic, IFormFile bannerpic, IFormFileCollection Images, IFormFileCollection VideosFiels, List<IFormFile> LifeeventMedia)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);


                user.Gender = userProfile.Gender;
                user.Bio = userProfile.Bio;
                user.Nationality = userProfile.Nationality;
                user.BirthDate = userProfile.BirthDate;
                user.Job = userProfile.Job;
                user.Qualification = userProfile.Qualification;
                user.FullName = userProfile.FullName;
                user.PhoneNumber = userProfile.PhoneNumber;
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
                user.Photos = userProfile.Photos == null ? new List<Photo>() : userProfile.Photos;
                user.Videos = userProfile.Videos == null ? new List<Video>() : userProfile.Videos;
                user.Skills = Skills == null ? new List<Skill>() : Skills;
                user.Interests = Interests == null ? new List<Interest>() : Interests;
                user.Educations = Educations == null ? new List<Education>() : Educations;
                user.Languages = Languages == null ? new List<Language>() : Languages;

                for (int i = 0; i < LifeeventMedia.Count(); i++)
                {
                    if (LifeeventMedia[i] != null)
                    {
                        string folder = "Images/ProfileImages/";
                        LifeEvents[i].Media = UploadImage(folder, LifeeventMedia[i]);
                        LifeEvents[i].Id = user.Id;
                    }
                    _applicationDbContext.LifeEvents.Add(LifeEvents[i]);
                }
                userProfile.LifeEvents = LifeEvents;
                user.LifeEvents = userProfile.LifeEvents == null ? new List<LifeEvent>() : userProfile.LifeEvents;

                if (Images.Count() > 0 && user.Photos.Count() == Images.Count())
                {
                    for (int i = 0; i < Images.Count(); i++)
                    {
                        Photo photo = new Photo();
                        if (Images[i] != null)
                        {
                            string folder = "Images/ProfileImages/";
                            photo.Image = UploadImage(folder, Images[i]);
                            photo.PublishDate = DateTime.Now;
                            //photo.Caption = userProfile.Photos[i].Caption;
                            photo.Id = user.Id;
                        }
                        _applicationDbContext.Photos.Add(photo);
                    }
                }
                if (VideosFiels.Count() > 0 && user.Videos.Count() == VideosFiels.Count())
                {
                    for (int i = 0; i < VideosFiels.Count(); i++)
                    {
                        Video video = new Video();
                        if (VideosFiels[i] != null)
                        {
                            string folder = "Videos/ProfileVideos/";
                            video.VideoT = UploadImage(folder, VideosFiels[i]);
                            //video.Caption = userProfile.Videos[i].Caption;
                            video.PublishDate = DateTime.Now;
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
                ToastNotification.AddSuccessToastMessage("Profile Updated Successfully..");
                return RedirectToPage("Profile");
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


    }

}

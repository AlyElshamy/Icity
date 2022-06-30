using Icity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Icity.Data
{
    public class ApplicationUser : IdentityUser
    {
        //public int EntityId { get; set; }
        //public int EntityName { get; set; }
        [Required(ErrorMessage = "Is Required")]
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public string Profilebanner { get; set; }
        public string Nationality { get; set; }

        public DateTime? BirthDate { get; set; }
        public string Bio { get; set; }
        public string Qualification { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }
        public string YoutubeLink { get; set; }
        public string FacebookLink { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string MapLocation { get; set; }
        public string NickName { get; set; }
        public string Phone2 { get; set; }
        public string MaritalStatus { get; set; }
        public string Website { get; set; }
        public int Folwers { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<LifeEvent> LifeEvents { get; set; }
        //public virtual ICollection<Vehicles> Vehicles { get; set; }

    }
    public class ApplicationDbContext : IdentityDbContext
    {
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<LifeEvent> LifeEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>()
            //    .Property(e => e.EntityId);

            //modelBuilder.Entity<ApplicationUser>()
            //    .Property(e => e.EntityName);
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.NickName).IsUnique();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Customer", NormalizedName = "Customer".ToUpper() });


        }
    }
}

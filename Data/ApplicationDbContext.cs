using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Icity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int EntityId { get; set; }
        public int EntityName { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public string Profilebanner { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Qualification { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext
    {
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.EntityId);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.EntityName);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Customer", NormalizedName = "Customer".ToUpper() });


        }
    }
}

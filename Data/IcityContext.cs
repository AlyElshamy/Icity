using System;
using Icity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Icity.Data
{
    public partial class IcityContext : DbContext
    {
        public IcityContext()
        {
        }
       

        public IcityContext(DbContextOptions<IcityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<AddListing> AddListings { get; set; }
        public virtual DbSet<ListingPhotos> ListingPhotos { get; set; }
        public virtual DbSet<ListingVideos> ListingVideos { get; set; }
        public virtual DbSet<ClassifiedAdsType> ClassifiedAdsTypes { get; set; }
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; }
        public virtual DbSet<ClassifiedAsdMedia> ClassifiedAsdMedias { get; set; }
        public virtual DbSet<ClassifiedAds> ClassifiedAds { get; set; }
        public virtual DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 1, CategoryTitleEn = "Business Support & Supplies" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId=2 , CategoryTitleEn = "Automotive"});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 3, CategoryTitleEn = "Computers & Electronics" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 4, CategoryTitleEn = "Construction & Contractors" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 5, CategoryTitleEn = "Education" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 6, CategoryTitleEn = "Entertainment" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 7, CategoryTitleEn = "Food & Dining" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 8, CategoryTitleEn = "Health & Medicine" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 9, CategoryTitleEn = "Home & Garden" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 10, CategoryTitleEn = "Legal & Financial" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 11, CategoryTitleEn = "Manufacturing, Wholesale, Distribution" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 12, CategoryTitleEn = "Merchants (Retail)" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 13, CategoryTitleEn = "Miscellaneous" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 14, CategoryTitleEn = "Personal Care & Services" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 15, CategoryTitleEn = "Real Estate" });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId= 16, CategoryTitleEn = "Travel & Transportation" });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=1, SubCategoryTitleEn= "Auto Accessories", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=2, SubCategoryTitleEn= "Auto Dealers – New", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=3, SubCategoryTitleEn= "Auto Dealers – Used", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=4, SubCategoryTitleEn= "Detail & Carwash", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=5, SubCategoryTitleEn= "Gas Stations", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=6, SubCategoryTitleEn= "Motorcycle Sales & Repair", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=7, SubCategoryTitleEn= "Rental & Leasing", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=8, SubCategoryTitleEn= "Service, Repair & Parts", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=9, SubCategoryTitleEn= "Towing", CategoryId=1});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=10, SubCategoryTitleEn= "Consultants", CategoryId=2});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=11, SubCategoryTitleEn= "Employment Agency", CategoryId=2});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=12, SubCategoryTitleEn= "Marketing & Communications", CategoryId=2});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=13, SubCategoryTitleEn= "Office Supplies", CategoryId=2});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=14, SubCategoryTitleEn= "Printing & Publishing", CategoryId=2});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=15, SubCategoryTitleEn= "Computer Programming & Support", CategoryId=3});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=16, SubCategoryTitleEn= "Consumer Electronics & Accessories", CategoryId=3});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=17, SubCategoryTitleEn = "Architects, Landscape Architects, Engineers & Surveyors", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=18,SubCategoryTitleEn= "Blasting & Demolition", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=19,SubCategoryTitleEn= "Construction Companies", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=20,SubCategoryTitleEn= "Electricians", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=21,SubCategoryTitleEn= "Engineer, Survey", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=22,SubCategoryTitleEn= "Environmental Assessments", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=23,SubCategoryTitleEn= "Inspectors", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=24,SubCategoryTitleEn= "Plaster & Concrete", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=25,SubCategoryTitleEn= "Plumbers", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=26,SubCategoryTitleEn= "Roofers", CategoryId=4});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=27,SubCategoryTitleEn= "Adult & Continuing Education", CategoryId=5});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=28,SubCategoryTitleEn= "Early Childhood Education", CategoryId=5});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=29,SubCategoryTitleEn= "Educational Resources", CategoryId=5});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=30,SubCategoryTitleEn= "Other Educational", CategoryId=5});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=32,SubCategoryTitleEn= "Artists, Writers", CategoryId=6});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=33,SubCategoryTitleEn= "Event Planners & Supplies", CategoryId=6});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=34,SubCategoryTitleEn= "Golf Courses", CategoryId=6});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=35,SubCategoryTitleEn= "Movies", CategoryId=6});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=36,SubCategoryTitleEn= "Productions", CategoryId=6});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=37,SubCategoryTitleEn= "Desserts, Catering & Supplies", CategoryId=7});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=38,SubCategoryTitleEn= "Fast Food & Carry Out", CategoryId=7});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=39,SubCategoryTitleEn= "Grocery, Beverage & Tobacco", CategoryId=7});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=40,SubCategoryTitleEn= "Restaurants", CategoryId=7});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=41,SubCategoryTitleEn= "Acupuncture", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=42,SubCategoryTitleEn= "Assisted Living & Home Health Care", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=43,SubCategoryTitleEn= "Audiologist", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=44,SubCategoryTitleEn= "Chiropractic", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=45,SubCategoryTitleEn= "Clinics & Medical Centers", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=46,SubCategoryTitleEn= "Dental ", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=47,SubCategoryTitleEn= "Diet I& Nutrition", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=48,SubCategoryTitleEn= "Laboratory, Imaging & Diagnostic", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=49,SubCategoryTitleEn= "Massage Therapy ", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=50,SubCategoryTitleEn= "Mental Health", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=51,SubCategoryTitleEn= "Nurse", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=52,SubCategoryTitleEn= "Optical", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=53,SubCategoryTitleEn= "Pharmacy, Drug & Vitamin Stores", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=54,SubCategoryTitleEn= "Physical Therapy", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=55,SubCategoryTitleEn= "Physicians & Assistants", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=56,SubCategoryTitleEn= "Podiatry", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=57,SubCategoryTitleEn= "Social Worker", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=58,SubCategoryTitleEn= "Animal Hospita", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=59,SubCategoryTitleEn= "Veterinary & Animal Surgeons ", CategoryId=8});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=60,SubCategoryTitleEn= "Antiques & Collectibles ", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=61,SubCategoryTitleEn= "Cleaning", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=62,SubCategoryTitleEn= "Crafts, Hobbies & Sports", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=63,SubCategoryTitleEn= "Flower Shops", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=64,SubCategoryTitleEn= "Home Furnishings", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=65,SubCategoryTitleEn= "Home Goods", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=66,SubCategoryTitleEn= "Home Improvements & Repairs", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=67,SubCategoryTitleEn= "Landscape & Lawn Service", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=68,SubCategoryTitleEn= "Pest Control", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=69,SubCategoryTitleEn= "Pool Supplies & Service", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=70,SubCategoryTitleEn= "Security System & Services", CategoryId=9});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=71,SubCategoryTitleEn= "Accountants ", CategoryId=10});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=72,SubCategoryTitleEn= "Attorneys", CategoryId=10});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=73,SubCategoryTitleEn= "Financial Institutions", CategoryId=10});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=74,SubCategoryTitleEn= "Financial Services", CategoryId=10});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=75,SubCategoryTitleEn= "Insurance", CategoryId=10});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=76,SubCategoryTitleEn= "Other Legal", CategoryId=10});
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=77,SubCategoryTitleEn = "Distribution, Import/Export", CategoryId = 11 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=78,SubCategoryTitleEn = "Manufacturing", CategoryId = 11 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=79,SubCategoryTitleEn = "Wholesale", CategoryId = 11 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=80,SubCategoryTitleEn = "Cards & Gifts", CategoryId = 12 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=81,SubCategoryTitleEn = "Clothing & Accessories", CategoryId = 12 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=82,SubCategoryTitleEn = "Department Stores, Sporting Goods", CategoryId = 12 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=83,SubCategoryTitleEn = "General", CategoryId = 12 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=84,SubCategoryTitleEn = "Jewelry", CategoryId = 12 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=85,SubCategoryTitleEn = "Shoes", CategoryId = 12 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=86,SubCategoryTitleEn = "Civic Groups", CategoryId = 13 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=87,SubCategoryTitleEn = "Funeral Service Providers & Cemetaries", CategoryId = 13 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=88,SubCategoryTitleEn = "Miscellaneous", CategoryId = 13 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=89,SubCategoryTitleEn = "Utility Companies", CategoryId = 13 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=90,SubCategoryTitleEn = "Animal Care & Supplies", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=91,SubCategoryTitleEn = "Barber & Beauty Salons", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=92,SubCategoryTitleEn = "Beauty Supplies", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=93,SubCategoryTitleEn = "Dry Cleaners & Laundromats", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=94,SubCategoryTitleEn = "Exercise & Fitness", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=95,SubCategoryTitleEn = "Massage & Body Works", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=96,SubCategoryTitleEn = "Nail Salons", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=97,SubCategoryTitleEn = "Shoe Repairs", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=98,SubCategoryTitleEn = "Agencies & Brokerage", CategoryId = 15 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=99,SubCategoryTitleEn = "Tailors", CategoryId = 14 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=100,SubCategoryTitleEn = "Agents & Brokers", CategoryId = 15 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=101,SubCategoryTitleEn = "Apartment & Home Rental", CategoryId = 15 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=102,SubCategoryTitleEn = "Mortgage Broker & Lender", CategoryId = 15 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=103,SubCategoryTitleEn = "Property Management", CategoryId = 15 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=104,SubCategoryTitleEn = "Title Company", CategoryId = 15 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=105,SubCategoryTitleEn = "Hotel, Motel & Extended Stay", CategoryId = 16 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=106,SubCategoryTitleEn = "Moving & Storage", CategoryId = 16 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=107,SubCategoryTitleEn = "Packaging & Shipping", CategoryId = 16 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory {SubCategoryID=108,SubCategoryTitleEn = "Transportation", CategoryId = 16 });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory { SubCategoryID =109, SubCategoryTitleEn = "Travel & Tourism", CategoryId = 16 });
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 1, TypeTitleEn = "Vehicles"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 2, TypeTitleEn = "Property"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 3, TypeTitleEn = "Electronics"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 4, TypeTitleEn = "Home&&Office Furniture – Decorations" });
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 5, TypeTitleEn = "Fashion"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 6, TypeTitleEn = "Pets"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 7, TypeTitleEn = "Kids"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 8, TypeTitleEn = "Books"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 9, TypeTitleEn = "Sports"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 10, TypeTitleEn = "Supermarket"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 11, TypeTitleEn = "Health&&Beauty" });
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 12, TypeTitleEn = "Gamming"});
            modelBuilder.Entity<ClassifiedAdsType>().HasData(new ClassifiedAdsType { ClassifiedAdsTypeID = 13, TypeTitleEn = "Accessories"});
            modelBuilder.Entity<ProductStatus>().HasData(new ProductStatus { ProductStatusID = 1, StatusTitle = "New"});
            modelBuilder.Entity<ProductStatus>().HasData(new ProductStatus { ProductStatusID = 2, StatusTitle = "Used"});



        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

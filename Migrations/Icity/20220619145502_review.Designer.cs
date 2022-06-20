﻿// <auto-generated />
using System;
using Icity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Icity.Migrations.Icity
{
    [DbContext(typeof(IcityContext))]
    [Migration("20220619145502_review")]
    partial class review
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Icity.Models.AddListing", b =>
                {
                    b.Property<int>("AddListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPeroson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainLocataion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddListingId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AddListings");
                });

            modelBuilder.Entity("Icity.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddListingId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BranchId");

                    b.HasIndex("AddListingId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Icity.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryTitleAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryTitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryTitleEn = "Business Support & Supplies"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryTitleEn = "Automotive"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryTitleEn = "Computers & Electronics"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryTitleEn = "Construction & Contractors"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryTitleEn = "Education"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryTitleEn = "Entertainment"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryTitleEn = "Food & Dining"
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryTitleEn = "Health & Medicine"
                        },
                        new
                        {
                            CategoryId = 9,
                            CategoryTitleEn = "Home & Garden"
                        },
                        new
                        {
                            CategoryId = 10,
                            CategoryTitleEn = "Legal & Financial"
                        },
                        new
                        {
                            CategoryId = 11,
                            CategoryTitleEn = "Manufacturing, Wholesale, Distribution"
                        },
                        new
                        {
                            CategoryId = 12,
                            CategoryTitleEn = "Merchants (Retail)"
                        },
                        new
                        {
                            CategoryId = 13,
                            CategoryTitleEn = "Miscellaneous"
                        },
                        new
                        {
                            CategoryId = 14,
                            CategoryTitleEn = "Personal Care & Services"
                        },
                        new
                        {
                            CategoryId = 15,
                            CategoryTitleEn = "Real Estate"
                        },
                        new
                        {
                            CategoryId = 16,
                            CategoryTitleEn = "Travel & Transportation"
                        });
                });

            modelBuilder.Entity("Icity.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddListingId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("AddListingId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Icity.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubCategoryTitleAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubCategoryTitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryID");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");

                    b.HasData(
                        new
                        {
                            SubCategoryID = 1,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Auto Accessories"
                        },
                        new
                        {
                            SubCategoryID = 2,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Auto Dealers – New"
                        },
                        new
                        {
                            SubCategoryID = 3,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Auto Dealers – Used"
                        },
                        new
                        {
                            SubCategoryID = 4,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Detail & Carwash"
                        },
                        new
                        {
                            SubCategoryID = 5,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Gas Stations"
                        },
                        new
                        {
                            SubCategoryID = 6,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Motorcycle Sales & Repair"
                        },
                        new
                        {
                            SubCategoryID = 7,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Rental & Leasing"
                        },
                        new
                        {
                            SubCategoryID = 8,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Service, Repair & Parts"
                        },
                        new
                        {
                            SubCategoryID = 9,
                            CategoryId = 1,
                            SubCategoryTitleEn = "Towing"
                        },
                        new
                        {
                            SubCategoryID = 10,
                            CategoryId = 2,
                            SubCategoryTitleEn = "Consultants"
                        },
                        new
                        {
                            SubCategoryID = 11,
                            CategoryId = 2,
                            SubCategoryTitleEn = "Employment Agency"
                        },
                        new
                        {
                            SubCategoryID = 12,
                            CategoryId = 2,
                            SubCategoryTitleEn = "Marketing & Communications"
                        },
                        new
                        {
                            SubCategoryID = 13,
                            CategoryId = 2,
                            SubCategoryTitleEn = "Office Supplies"
                        },
                        new
                        {
                            SubCategoryID = 14,
                            CategoryId = 2,
                            SubCategoryTitleEn = "Printing & Publishing"
                        },
                        new
                        {
                            SubCategoryID = 15,
                            CategoryId = 3,
                            SubCategoryTitleEn = "Computer Programming & Support"
                        },
                        new
                        {
                            SubCategoryID = 16,
                            CategoryId = 3,
                            SubCategoryTitleEn = "Consumer Electronics & Accessories"
                        },
                        new
                        {
                            SubCategoryID = 17,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Architects, Landscape Architects, Engineers & Surveyors"
                        },
                        new
                        {
                            SubCategoryID = 18,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Blasting & Demolition"
                        },
                        new
                        {
                            SubCategoryID = 19,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Construction Companies"
                        },
                        new
                        {
                            SubCategoryID = 20,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Electricians"
                        },
                        new
                        {
                            SubCategoryID = 21,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Engineer, Survey"
                        },
                        new
                        {
                            SubCategoryID = 22,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Environmental Assessments"
                        },
                        new
                        {
                            SubCategoryID = 23,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Inspectors"
                        },
                        new
                        {
                            SubCategoryID = 24,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Plaster & Concrete"
                        },
                        new
                        {
                            SubCategoryID = 25,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Plumbers"
                        },
                        new
                        {
                            SubCategoryID = 26,
                            CategoryId = 4,
                            SubCategoryTitleEn = "Roofers"
                        },
                        new
                        {
                            SubCategoryID = 27,
                            CategoryId = 5,
                            SubCategoryTitleEn = "Adult & Continuing Education"
                        },
                        new
                        {
                            SubCategoryID = 28,
                            CategoryId = 5,
                            SubCategoryTitleEn = "Early Childhood Education"
                        },
                        new
                        {
                            SubCategoryID = 29,
                            CategoryId = 5,
                            SubCategoryTitleEn = "Educational Resources"
                        },
                        new
                        {
                            SubCategoryID = 30,
                            CategoryId = 5,
                            SubCategoryTitleEn = "Other Educational"
                        },
                        new
                        {
                            SubCategoryID = 32,
                            CategoryId = 6,
                            SubCategoryTitleEn = "Artists, Writers"
                        },
                        new
                        {
                            SubCategoryID = 33,
                            CategoryId = 6,
                            SubCategoryTitleEn = "Event Planners & Supplies"
                        },
                        new
                        {
                            SubCategoryID = 34,
                            CategoryId = 6,
                            SubCategoryTitleEn = "Golf Courses"
                        },
                        new
                        {
                            SubCategoryID = 35,
                            CategoryId = 6,
                            SubCategoryTitleEn = "Movies"
                        },
                        new
                        {
                            SubCategoryID = 36,
                            CategoryId = 6,
                            SubCategoryTitleEn = "Productions"
                        },
                        new
                        {
                            SubCategoryID = 37,
                            CategoryId = 7,
                            SubCategoryTitleEn = "Desserts, Catering & Supplies"
                        },
                        new
                        {
                            SubCategoryID = 38,
                            CategoryId = 7,
                            SubCategoryTitleEn = "Fast Food & Carry Out"
                        },
                        new
                        {
                            SubCategoryID = 39,
                            CategoryId = 7,
                            SubCategoryTitleEn = "Grocery, Beverage & Tobacco"
                        },
                        new
                        {
                            SubCategoryID = 40,
                            CategoryId = 7,
                            SubCategoryTitleEn = "Restaurants"
                        },
                        new
                        {
                            SubCategoryID = 41,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Acupuncture"
                        },
                        new
                        {
                            SubCategoryID = 42,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Assisted Living & Home Health Care"
                        },
                        new
                        {
                            SubCategoryID = 43,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Audiologist"
                        },
                        new
                        {
                            SubCategoryID = 44,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Chiropractic"
                        },
                        new
                        {
                            SubCategoryID = 45,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Clinics & Medical Centers"
                        },
                        new
                        {
                            SubCategoryID = 46,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Dental "
                        },
                        new
                        {
                            SubCategoryID = 47,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Diet I& Nutrition"
                        },
                        new
                        {
                            SubCategoryID = 48,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Laboratory, Imaging & Diagnostic"
                        },
                        new
                        {
                            SubCategoryID = 49,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Massage Therapy "
                        },
                        new
                        {
                            SubCategoryID = 50,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Mental Health"
                        },
                        new
                        {
                            SubCategoryID = 51,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Nurse"
                        },
                        new
                        {
                            SubCategoryID = 52,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Optical"
                        },
                        new
                        {
                            SubCategoryID = 53,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Pharmacy, Drug & Vitamin Stores"
                        },
                        new
                        {
                            SubCategoryID = 54,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Physical Therapy"
                        },
                        new
                        {
                            SubCategoryID = 55,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Physicians & Assistants"
                        },
                        new
                        {
                            SubCategoryID = 56,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Podiatry"
                        },
                        new
                        {
                            SubCategoryID = 57,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Social Worker"
                        },
                        new
                        {
                            SubCategoryID = 58,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Animal Hospita"
                        },
                        new
                        {
                            SubCategoryID = 59,
                            CategoryId = 8,
                            SubCategoryTitleEn = "Veterinary & Animal Surgeons "
                        },
                        new
                        {
                            SubCategoryID = 60,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Antiques & Collectibles "
                        },
                        new
                        {
                            SubCategoryID = 61,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Cleaning"
                        },
                        new
                        {
                            SubCategoryID = 62,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Crafts, Hobbies & Sports"
                        },
                        new
                        {
                            SubCategoryID = 63,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Flower Shops"
                        },
                        new
                        {
                            SubCategoryID = 64,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Home Furnishings"
                        },
                        new
                        {
                            SubCategoryID = 65,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Home Goods"
                        },
                        new
                        {
                            SubCategoryID = 66,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Home Improvements & Repairs"
                        },
                        new
                        {
                            SubCategoryID = 67,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Landscape & Lawn Service"
                        },
                        new
                        {
                            SubCategoryID = 68,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Pest Control"
                        },
                        new
                        {
                            SubCategoryID = 69,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Pool Supplies & Service"
                        },
                        new
                        {
                            SubCategoryID = 70,
                            CategoryId = 9,
                            SubCategoryTitleEn = "Security System & Services"
                        },
                        new
                        {
                            SubCategoryID = 71,
                            CategoryId = 10,
                            SubCategoryTitleEn = "Accountants "
                        },
                        new
                        {
                            SubCategoryID = 72,
                            CategoryId = 10,
                            SubCategoryTitleEn = "Attorneys"
                        },
                        new
                        {
                            SubCategoryID = 73,
                            CategoryId = 10,
                            SubCategoryTitleEn = "Financial Institutions"
                        },
                        new
                        {
                            SubCategoryID = 74,
                            CategoryId = 10,
                            SubCategoryTitleEn = "Financial Services"
                        },
                        new
                        {
                            SubCategoryID = 75,
                            CategoryId = 10,
                            SubCategoryTitleEn = "Insurance"
                        },
                        new
                        {
                            SubCategoryID = 76,
                            CategoryId = 10,
                            SubCategoryTitleEn = "Other Legal"
                        },
                        new
                        {
                            SubCategoryID = 77,
                            CategoryId = 11,
                            SubCategoryTitleEn = "Distribution, Import/Export"
                        },
                        new
                        {
                            SubCategoryID = 78,
                            CategoryId = 11,
                            SubCategoryTitleEn = "Manufacturing"
                        },
                        new
                        {
                            SubCategoryID = 79,
                            CategoryId = 11,
                            SubCategoryTitleEn = "Wholesale"
                        },
                        new
                        {
                            SubCategoryID = 80,
                            CategoryId = 12,
                            SubCategoryTitleEn = "Cards & Gifts"
                        },
                        new
                        {
                            SubCategoryID = 81,
                            CategoryId = 12,
                            SubCategoryTitleEn = "Clothing & Accessories"
                        },
                        new
                        {
                            SubCategoryID = 82,
                            CategoryId = 12,
                            SubCategoryTitleEn = "Department Stores, Sporting Goods"
                        },
                        new
                        {
                            SubCategoryID = 83,
                            CategoryId = 12,
                            SubCategoryTitleEn = "General"
                        },
                        new
                        {
                            SubCategoryID = 84,
                            CategoryId = 12,
                            SubCategoryTitleEn = "Jewelry"
                        },
                        new
                        {
                            SubCategoryID = 85,
                            CategoryId = 12,
                            SubCategoryTitleEn = "Shoes"
                        },
                        new
                        {
                            SubCategoryID = 86,
                            CategoryId = 13,
                            SubCategoryTitleEn = "Civic Groups"
                        },
                        new
                        {
                            SubCategoryID = 87,
                            CategoryId = 13,
                            SubCategoryTitleEn = "Funeral Service Providers & Cemetaries"
                        },
                        new
                        {
                            SubCategoryID = 88,
                            CategoryId = 13,
                            SubCategoryTitleEn = "Miscellaneous"
                        },
                        new
                        {
                            SubCategoryID = 89,
                            CategoryId = 13,
                            SubCategoryTitleEn = "Utility Companies"
                        },
                        new
                        {
                            SubCategoryID = 90,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Animal Care & Supplies"
                        },
                        new
                        {
                            SubCategoryID = 91,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Barber & Beauty Salons"
                        },
                        new
                        {
                            SubCategoryID = 92,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Beauty Supplies"
                        },
                        new
                        {
                            SubCategoryID = 93,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Dry Cleaners & Laundromats"
                        },
                        new
                        {
                            SubCategoryID = 94,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Exercise & Fitness"
                        },
                        new
                        {
                            SubCategoryID = 95,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Massage & Body Works"
                        },
                        new
                        {
                            SubCategoryID = 96,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Nail Salons"
                        },
                        new
                        {
                            SubCategoryID = 97,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Shoe Repairs"
                        },
                        new
                        {
                            SubCategoryID = 98,
                            CategoryId = 15,
                            SubCategoryTitleEn = "Agencies & Brokerage"
                        },
                        new
                        {
                            SubCategoryID = 99,
                            CategoryId = 14,
                            SubCategoryTitleEn = "Tailors"
                        },
                        new
                        {
                            SubCategoryID = 100,
                            CategoryId = 15,
                            SubCategoryTitleEn = "Agents & Brokers"
                        },
                        new
                        {
                            SubCategoryID = 101,
                            CategoryId = 15,
                            SubCategoryTitleEn = "Apartment & Home Rental"
                        },
                        new
                        {
                            SubCategoryID = 102,
                            CategoryId = 15,
                            SubCategoryTitleEn = "Mortgage Broker & Lender"
                        },
                        new
                        {
                            SubCategoryID = 103,
                            CategoryId = 15,
                            SubCategoryTitleEn = "Property Management"
                        },
                        new
                        {
                            SubCategoryID = 104,
                            CategoryId = 15,
                            SubCategoryTitleEn = "Title Company"
                        },
                        new
                        {
                            SubCategoryID = 105,
                            CategoryId = 16,
                            SubCategoryTitleEn = "Hotel, Motel & Extended Stay"
                        },
                        new
                        {
                            SubCategoryID = 106,
                            CategoryId = 16,
                            SubCategoryTitleEn = "Moving & Storage"
                        },
                        new
                        {
                            SubCategoryID = 107,
                            CategoryId = 16,
                            SubCategoryTitleEn = "Packaging & Shipping"
                        },
                        new
                        {
                            SubCategoryID = 108,
                            CategoryId = 16,
                            SubCategoryTitleEn = "Transportation"
                        },
                        new
                        {
                            SubCategoryID = 109,
                            CategoryId = 16,
                            SubCategoryTitleEn = "Travel & Tourism"
                        });
                });

            modelBuilder.Entity("Icity.Models.AddListing", b =>
                {
                    b.HasOne("Icity.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Icity.Models.Branch", b =>
                {
                    b.HasOne("Icity.Models.AddListing", "AddListing")
                        .WithMany("Branches")
                        .HasForeignKey("AddListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddListing");
                });

            modelBuilder.Entity("Icity.Models.Review", b =>
                {
                    b.HasOne("Icity.Models.AddListing", "AddListing")
                        .WithMany("Reviews")
                        .HasForeignKey("AddListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddListing");
                });

            modelBuilder.Entity("Icity.Models.SubCategory", b =>
                {
                    b.HasOne("Icity.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Icity.Models.AddListing", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Icity.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

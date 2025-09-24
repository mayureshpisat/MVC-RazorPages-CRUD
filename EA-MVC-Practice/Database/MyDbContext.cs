using EA_MVC_Practice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace EA_MVC_Practice.Database
{
    public class MyDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    { 

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(100).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}

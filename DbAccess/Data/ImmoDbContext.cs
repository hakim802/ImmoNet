using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccess.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbAccess.Data
{
    public class ImmoDbContext : IdentityDbContext<Contact>
    {
        public ImmoDbContext(DbContextOptions<ImmoDbContext> options) : base(options)
        {

        }


        public DbSet<Immo> Immos { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Contacts").Property(p => p.Id).HasColumnName("Id"); ;
            //modelBuilder.Entity<ApplicationUser>().ToTable("Contacts").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

        }
    }
}

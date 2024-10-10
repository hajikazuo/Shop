using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "2f4580ed-9dff-491c-9405-83b1f41261b0";
            var vendorRoleId = "3b2630e2-a9d3-46a6-92cd-6a34647812de";
            var clientRoleId = "2f4580ed-9dff-491c-9405-83b1f41261b1";
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = clientRoleId,
                    Name = "Client",
                    NormalizedName = "Client".ToUpper(),
                    ConcurrencyStamp = clientRoleId
                },
                new IdentityRole()
                {
                    Id = vendorRoleId,
                    Name = "Vendor",
                    NormalizedName = "Vendor".ToUpper(),
                    ConcurrencyStamp = vendorRoleId
                },
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = adminRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "d904e73e-b385-4cfc-bcd1-98ef15cb52c4";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@shop.com",
                Email = "admin@shop.com",
                NormalizedUserName = "admin@shop.com".ToUpper(),
                NormalizedEmail = "admin@shop.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123!");

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = clientRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = vendorRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}

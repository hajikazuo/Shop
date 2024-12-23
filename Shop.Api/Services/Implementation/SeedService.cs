﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Services.Interface;
using Shop.Common.Context;
using Shop.Common.Models.Entities;
using Shop.Common.Models.Entities.Users;
using System;

namespace Shop.Api.Services.Implementation
{
    public class SeedService : ISeedService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;

        private const string ClientRole = "Client";
        private const string VendorRole = "Vendor";
        private const string AdminRole = "Admin";

        private Guid CategoryId = new Guid("a2ee5dde-6af8-42b1-8faf-11d58b01e258");

        public SeedService(UserManager<User> userManager, RoleManager<Role> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Seed()
        {
            CreateRole(ClientRole).GetAwaiter().GetResult();
            CreateRole(VendorRole).GetAwaiter().GetResult();
            CreateRole(AdminRole).GetAwaiter().GetResult();
            CreateUser("admin@shop.com", "Admin", "Test@2024!", roles: new List<string> { ClientRole, VendorRole, AdminRole }).GetAwaiter().GetResult();

            CreateCategory().GetAwaiter().GetResult();
            CreateProducts().GetAwaiter().GetResult();
        }

        private async Task<IdentityResult> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role
                {
                    Name = roleName
                };
                return await _roleManager.CreateAsync(role);
            }
            return default;
        }

        private async Task<IdentityResult> CreateUser(string email, string name, string password, IEnumerable<string> roles)
        {
            var request = await _userManager.FindByEmailAsync(email);
            if (request == null)
            {
                var user = new User
                {
                    UserName = email,
                    Email = email,
                    Name = name,
                    EmailConfirmed = true,
                };

                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, roles);
                }

                return result;
            }
            else
            {
                return default;
            }
        }

        private async Task CreateCategory()
        {
            var exists = await _context.Categories.AnyAsync();
            if (exists != true)
            {
                var entity = new Category
                {
                    CategoryId = CategoryId,
                    Name = "Smartphones"
                };
                _context.Categories?.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateProducts()
        {
            var exists = await _context.Products.AnyAsync();
            if (!exists)
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Smartphone Galaxy S23",
                        Price = 4999.99m,
                        Description = "Smartphone Galaxy S23 com tela de 6.1 polegadas, 128GB de armazenamento, 8GB de RAM, câmera de 50MP e processador Snapdragon 8 Gen 2.",
                        Stock = 100,
                        ImageURL = "smartphone-galaxy-s23.jpg",
                        CategoryId = CategoryId,
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "iPhone 14 Pro Max",
                        Price = 7999.99m,
                        Description = "iPhone 14 Pro Max com tela Super Retina XDR de 6.7 polegadas, 256GB de armazenamento, câmera tripla de 48MP, e chip A16 Bionic.",
                        Stock = 50,
                        ImageURL = "iphone-14-pro-max.jpg",
                        CategoryId = CategoryId,
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Xiaomi 13 Ultra",
                        Price = 5999.99m,
                        Description = "Xiaomi 13 Ultra com tela AMOLED de 6.73 polegadas, 512GB de armazenamento, câmera Leica de 50MP e processador Snapdragon 8 Gen 2.",
                        Stock = 75,
                        ImageURL = "xiaomi-13-ultra.jpg",
                        CategoryId = CategoryId,
                    }
                };

                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            }
        }

    }
}

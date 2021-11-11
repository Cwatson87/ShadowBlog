using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShadowBlog.Data;
using ShadowBlog.Models;
using ShadowBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;
        private readonly UserManager<BlogUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISlugService _slugService;

        public DataService(ApplicationDbContext dbContext,
            IImageService imageService,
            UserManager<BlogUser> userManager,
            RoleManager<IdentityRole> roleManager, ISlugService slugService)
        {
            _dbContext = dbContext;
            _imageService = imageService;
            _userManager = userManager;
            _roleManager = roleManager;
            _slugService = slugService;
        }

        //I am free to use _dbContext anywhere inside the class...
        public async Task ManageDataAsync()
        {
            await _dbContext.Database.MigrateAsync();

            //Step 1: Create 2 Roles
            await SeedRolesAsync();

            //Step 2: Create 2 Users and assign the roles
            await SeedUsersAsync();

            //Step 3: Seed Blogs -- For paging purposes...
            await SeedBlogsAsync();

        }
        private async Task SeedRolesAsync()
        {
            //Ask if there are any Roles in the AspNetRoles table
            if (_dbContext.Roles.Any())
            {
                return;
            }

            IdentityRole adminRole = new("Administrator");
            await _roleManager.CreateAsync(adminRole);

            IdentityRole moderatorRole = new("Moderator");
            await _roleManager.CreateAsync(moderatorRole);
        }

        private async Task SeedUsersAsync()
        {
            //Ask if there are any Users at all already in the AspNetUsers table
            if (_dbContext.Users.Any()) return;

            BlogUser admin = new()
            {
                Email = "snivley12@mailinator.com",
                UserName = "snivley12@Mailinator.com",
                FirstName = "Cameron",
                LastName = "Watson",
                PhoneNumber = "867-5309",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("DefaultNewUser.png"),
                ImageType = "png, img"
            };

            await _userManager.CreateAsync(admin, "Abc&123!");
            await _userManager.AddToRoleAsync(admin, "Administrator");
            await _userManager.AddToRoleAsync(admin, "Moderator");

            //TODO: Now Seed a User who will occupy the Moderator role
        }

        private async Task SeedBlogsAsync()
        {
            if (_dbContext.Blogs.Any())
                return;

            for (var loop = 1; loop <= 20; loop++)
            {
                _dbContext.Add(new Blog()
                {
                    Name = $"Blog For Application {loop}",
                    Description = $"Everything I learned while building Application {loop}",
                    Created = DateTime.Now.AddDays(loop),
                    ImageData = await _imageService.EncodeImageAsync("defaultBlog.jpg"),
                    ContentType = "jpg"
                });
            }
            await _dbContext.SaveChangesAsync();
        }

    }
}
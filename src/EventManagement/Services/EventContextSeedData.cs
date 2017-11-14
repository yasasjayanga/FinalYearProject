using EventManagement.data;
using EventManagement.Data;
using EventManagement.Models;
using EventManagement.service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Services
{
    public class EventContextSeedData
    {
        private ApplicationDbContext _context;

        public EventContextSeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void SeedAdminUser()
        {
            var user = new ApplicationUser
            {
                UserName = "admin@email.com",
                NormalizedUserName = "admin@email.com",
                Email = "admin@email.com",
                NormalizedEmail = "admin@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
            }

            if (!_context.Roles.Any(r => r.Name == "user"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "user", NormalizedName = "user" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "12345");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                var aspUser = await userStore.FindByNameAsync(user.UserName);
                await userStore.AddToRoleAsync(aspUser, "admin");

                var userInfo = new User
                {
                    FirstName = "admin",
                    UserId = aspUser.Id,
                    LastName = "admin",
                    Gender = 1,
                    Dob = DateTime.Now,
                    ContactNumber = "985745896",
                    Address = "USA",
                    City = "los",
                    Country = 1,
                    ProfilePhoto = ""
                };
                
                new UsersService().SaveUser(userInfo);
            }
            await _context.SaveChangesAsync();
        }
    }
}

﻿using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public static class DbInitializer
    {
        public static AppSecrets appSecrets { get; set; }
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1; 

            // should log an error message here
            // Seed roles

            int result = await SeedRoles(roleManager);

            if (result != 0)
                return 2;

            // should log an error message here
            // Check if users already exist and exit if there are
            
            if (userManager.Users.Count() > 0)
                
                return 3;

            // should log an error message here
            // Seed users
            result = await SeedUsers(userManager);

            if (result != 0)
                return 4;  // should log an error message here
            return 0;
            
            
        }
        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Managers"));
            if (!result.Succeeded)
                return 1;  
            
            // should log an error message here
            // Create Member Role
            
            
            result = await roleManager.CreateAsync(new IdentityRole("Players"));
            if (!result.Succeeded)
                return 2; 
            
            // should log an error message here
            return 0;
        }
        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            Console.WriteLine("Seeding users");
            // Create Admin User
            var managerUser = new ApplicationUser
            {
                UserName = "team.manager@mohawkcollege.ca",
                Email = "team.manager@mohawkcollege.ca",
                FirstName = "Team",
                LastName = "Admin",
                PhoneNumber = "905-555-5555",
                EmailConfirmed = true
            };


            var result = await userManager.CreateAsync(managerUser, appSecrets.ManagerPwd);
            if (!result.Succeeded)
                return 1;  
            
            // should log an error message here        
            // Assign user to Admin role

            result = await userManager.AddToRoleAsync(managerUser, "Managers");
            if (!result.Succeeded)
                return 2;  
            
            // should log an error message here
            // Create Member User


            
            var playerUser = new ApplicationUser
            {
                UserName = "the.player@mohawkcollege.ca",
                Email = "the.player@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Player",
                PhoneNumber = "905-555-5555",
                EmailConfirmed = true
            };


            result = await userManager.CreateAsync(playerUser, appSecrets.PlayerPwd);
            if (!result.Succeeded)
                return 3; 
            
            
            // should log an error message here
            // Assign user to Member role
            

            
            result = await userManager.AddToRoleAsync(playerUser, "Players");
            if (!result.Succeeded)
                return 4;  // should log an error message here
            return 0;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace L10_2.Data
{
    public class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        // name - correct email
        // password - min 8 charcters, small and capital letter, digit and special char

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Client").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Client",
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedOneUser(UserManager<IdentityUser> userManager,
                                        string name, string password, string role = null)
        {
            if (userManager.FindByNameAsync(name).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = name, // the same like the email
                    Email = name
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded && role != null)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            SeedOneUser(userManager, "admin@localhost", "Aadmin01!", "Admin");
        }
    }
}

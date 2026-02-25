using GemManagment.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GemManagment.DAL.Data.Context
{
    public class GemDbInitlizer
    {
        private readonly GemDbcontext dbcontext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public GemDbInitlizer(GemDbcontext dbcontext , RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.dbcontext = dbcontext;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public bool SeedDate()
        {
            try
            {
                var HasPlans = dbcontext.Plans.Any();
                var HasCategories = dbcontext.Category.Any();
                if (!HasPlans)
                {
                    var plans = LoadDataFromJson<Plans>("Plans.json");
                    dbcontext.Plans.AddRange(plans);
                }
                if (!HasCategories)
                {
                    var categories = LoadDataFromJson<Category>("Categories.json");
                    dbcontext.Category.AddRange(categories);
                }
                return dbcontext.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }


        }

        public async Task<bool> SeedIdentity()
        {
            var HasUser = userManager.Users.Any();
            var HasRoles = roleManager.Roles.Any();
            if (HasRoles && HasUser) return false;
            if (!HasRoles)
            {
                var roles = new IdentityRole[]
                {
                     new IdentityRole { Name = "Admin" },
                     new IdentityRole { Name = "SuperAdmin" }
                };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        var result = await roleManager.CreateAsync(role);
                        if (!result.Succeeded)
                        {
                            throw new Exception($"Failed to create role {role.Name}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }

            }
            if (!HasUser)
            {
                var user1 = new ApplicationUser
                {
                    FirstName = "mohamed gamal",
                    UserName = "SuperAdmin",
                    Email = "mohamed@gmail.com"
                };
                var user2 = new ApplicationUser
                {
                    FirstName = "ahmed gamal",
                    UserName = "Admin",
                    Email = "ahmed@gmail.com"
                };
                await userManager.CreateAsync(user1, "P@ssw0rd");
                await userManager.CreateAsync(user2, "P@ssw0rd");
                await userManager.AddToRoleAsync(user1, "SuperAdmin");
                await userManager.AddToRoleAsync(user2, "Admin");
            }
            return true;
        }
        private List<T> LoadDataFromJson<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {fileName} was not found at path {filePath}");
            }
            var jsonData = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                throw new Exception($"The file {fileName} is empty.");
            }
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<T>>(jsonData, jsonSerializerOptions);
            if (data == null || data.Count == 0)
            {
                throw new Exception($"Failed to deserialize data from {fileName}");
            }
            return data;
        }

        public void Initialize()
        {
            try
            {
                var pendingMigrations = dbcontext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    dbcontext.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw new Exception("Can not apply migration");
            }

        }
    }
}

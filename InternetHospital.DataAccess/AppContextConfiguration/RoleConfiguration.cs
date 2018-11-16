using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class RoleConfiguration
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            try
            {
                var initialRoles = new List<IdentityRole<int>>
                {
                    new IdentityRole<int>
                    {
                        Name = "Patient"
                    }
                    ,new IdentityRole<int>
                    {
                        Name = "Doctor"
                    }
                    ,new IdentityRole<int>
                    {
                        Name = "Moderator"
                    }
                    ,new IdentityRole<int>
                    {
                        Name = "Admin"
                    }
                };

                foreach (var role in initialRoles)
                {
                    if (await roleManager.FindByNameAsync(role.Name) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(role.Name));
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Check if file exist and path is correct: {ex.Message}");
                throw;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Something wrong with file or path, its better to check them: {ex.Message}");
                throw;
            }
        }
    }
}
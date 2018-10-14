using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class RoleConfiguration
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            string Patient = "Patient";
            string Doctor = "Doctor";
            string Moderator = "Moderator";
            string Admin = "Admin";

         
            if (await roleManager.FindByNameAsync(Patient) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Patient));
            }
            if (await roleManager.FindByNameAsync(Doctor) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Doctor));
            }
            if (await roleManager.FindByNameAsync(Moderator) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Moderator));
            }
            if (await roleManager.FindByNameAsync(Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Admin));
            }
        }
    }
}

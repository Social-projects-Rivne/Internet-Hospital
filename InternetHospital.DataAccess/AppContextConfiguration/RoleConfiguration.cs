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
        public static async Task InitializeAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            const string PATIENT = "Patient";
            const string DOCTOR = "Doctor";
            const string MODERATOR = "Moderator";
            const string ADMIN = "Admin";
         
            if (await roleManager.FindByNameAsync(PATIENT) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(PATIENT));
            }
            if (await roleManager.FindByNameAsync(DOCTOR) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(DOCTOR));
            }
            if (await roleManager.FindByNameAsync(MODERATOR) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(MODERATOR));
            }
            if (await roleManager.FindByNameAsync(ADMIN) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(ADMIN));
            }
        }
    }
}

using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class User_RoleConfiguration
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            #region DefaultCredentionals
            string userEmail = "user@gmail.com";
            string userPassword = "1111aA1111&";

            string patientEmail = "patient@gmail.com";
            string patientPassword = "2222aA2222&";

            string doctorEmail = "doctor@gmail.com";
            string doctorPassword = "3333aA3333&";
            #endregion

            #region RoleInitialize
            if (await roleManager.FindByNameAsync("Patient") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Patient"));
            }
            if (await roleManager.FindByNameAsync("Doctor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Doctor"));
            }
            if (await roleManager.FindByNameAsync("Moderator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Moderator"));
            }
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            }
            #endregion

            #region UserInitialize
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User user = new User { Email = userEmail, UserName = userEmail, StatusId = 2 };
                IdentityResult result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Patient");
                }
            }

            if (await userManager.FindByNameAsync(patientEmail) == null)
            {
                User patient = new User { Email = patientEmail, UserName = patientEmail, StatusId = 2 };
                IdentityResult result = await userManager.CreateAsync(patient, patientPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(patient, "Patient");
                }
            }

            if (await userManager.FindByNameAsync(doctorEmail) == null)
            {
                User doctor = new User { Email = doctorEmail,UserName = doctorEmail, StatusId = 3 };
                IdentityResult result = await userManager.CreateAsync(doctor, doctorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctor, "Doctor");
                }
            }
            #endregion
        }
    }
}

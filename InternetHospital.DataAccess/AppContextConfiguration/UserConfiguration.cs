using Bogus;
using Bogus.DataSets;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class UserConfiguration
    {
        private enum Gender
        {
            Male,
            Female
        }

        private static Random rng = new Random();

        public static async Task InitializeAsync(UserManager<User> userManager)
        {
            const int USERS_AMOUNT = 700;
            const int NO_DOCTOR_USERS = 300;
            int moderatorsAmount = 60;
            #region FakeUserData
            string password = "1234Pass";
            var fakeUsers = new Faker<User>()
                            .RuleFor(u => u.FirstName, f => f.Name.FirstName(Name.Gender.Male))
                            .RuleFor(u => u.SecondName, f => f.Name.LastName(Name.Gender.Male))
                            .RuleFor(u => u.ThirdName, f => f.Name.FirstName(Name.Gender.Male))
                            .RuleFor(u => u.BirthDate, f => f.Date.Between(new DateTime(1989, 1, 1), DateTime.Now))
                            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.SecondName))
                            .RuleFor(u => u.UserName, (f, u) => u.Email)
                            .RuleFor(u => u.EmailConfirmed, f => true)
                            .RuleFor(u => u.SignUpTime, f => DateTime.Now)
                            .RuleFor(u => u.LastStatusChangeTime, f => DateTime.Now)
                            .RuleFor(u => u.StatusId, f => 3)
                            .RuleFor(u => u.Doctor, f => new Doctor
                            {
                                Address = f.Address.FullAddress(),
                                IsApproved = true,
                                SpecializationId = rng.Next(1, 34),
                                DoctorsInfo = "Hello, aloha, czesz!! I am a great doctor!"
                            });
            #endregion

            var users = fakeUsers.Generate(USERS_AMOUNT + 1);

            for (int i = 0; i < NO_DOCTOR_USERS + 1; i++)
            {
                users[i].Doctor = null;
            }

            if (await userManager.FindByNameAsync(users[0].Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(users[0], password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(users[0], "Admin");
                }
            }

            for (int i = 1; i < moderatorsAmount; i++)
            {
                if (await userManager.FindByNameAsync(users[i].Email) == null)
                {
                    IdentityResult result = await userManager.CreateAsync(users[i], password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(users[i], "Moderator");
                    }
                }
            }

            for (int i = moderatorsAmount; i < USERS_AMOUNT; i++)
            {
                if (await userManager.FindByNameAsync(users[i].Email) == null)
                {
                    IdentityResult result = await userManager.CreateAsync(users[i], password);
                    if (users[i].Doctor != null)
                    {
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(users[i], "Doctor");
                        }
                    }
                    else
                    {
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(users[i], "Patient");
                        }
                    }
                }
            }
        }
    }
}
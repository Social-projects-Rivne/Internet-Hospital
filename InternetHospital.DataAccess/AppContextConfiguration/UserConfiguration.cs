using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class UserConfiguration
    {
        public static async Task InitializeAsync(UserManager<User> userManager)
        {
            try
            {
                const string PATH = @"..\InternetHospital.DataAccess\AppContextConfiguration\UserConfigurationJSON.json";
                string jsonString = File.ReadAllText(PATH);
                var jsonUsers = JArray.Parse(jsonString);

                //It's necessary because i had an exception in runtime execution 
                string Email = null;
                string FirstName = null;
                string SecondName = null;
                string ThirdName = null;
                dynamic Doctor = null;
                string Password = null;
                string Role = null;
                string StatusId = null;

                foreach (dynamic item in jsonUsers)
                {
                    Email = item.Email;
                    if (await userManager.FindByNameAsync(Email) == null)
                    {
                        FirstName = item.FirstName;
                        SecondName = item.SecondName;
                        ThirdName = item.ThirdName;
                        StatusId = item.StatusId;
                        Doctor = item.Doctor;
                        Password = item.Password;
                        Role = item.Role;

                        var user = new User {
                            UserName = Email,
                            Email = Email,
                            EmailConfirmed = true,
                            FirstName = FirstName,
                            SecondName = SecondName,
                            ThirdName = ThirdName,
                            StatusId = Int32.Parse(StatusId),
                            SignUpTime = DateTime.Now,
                            LastStatusChangeTime = DateTime.Now
                        };

                        if(Doctor != null)
                        {
                            string DoctorsInfo = item.Doctor.DoctorsInfo;
                            string Address = item.Doctor.Address;
                            string IsApproved = item.Doctor.IsApproved;

                            user.Doctor = new Doctor {
                                DoctorsInfo = DoctorsInfo,
                                Address = Address,
                                IsApproved = Boolean.Parse(IsApproved)
                            };
                        }

                        IdentityResult result = await userManager.CreateAsync(user, Password);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, Role);
                        }
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

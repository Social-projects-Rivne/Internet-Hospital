using InternetHospital.DataAccess.AppContextConfiguration.Helpers;
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
                string jsonString = File.ReadAllText(UrlHelper.JsonFilesURL + UrlHelper.UserConfigJSON);
                var jsonUsers = JArray.Parse(jsonString);

                //It's necessary because i had an exception in runtime execution 
                string email = null;
                string firstName = null;
                string secondName = null;
                string thirdName = null;
                dynamic doctor = null;
                string password = null;
                string role = null;
                string statusId = null;

                foreach (dynamic item in jsonUsers)
                {
                    email = item.Email;
                    if (await userManager.FindByNameAsync(email) == null)
                    {
                        firstName = item.FirstName;
                        secondName = item.SecondName;
                        thirdName = item.ThirdName;
                        statusId = item.StatusId;
                        doctor = item.Doctor;
                        password = item.Password;
                        role = item.Role;

                        if (!int.TryParse(statusId, out int stId))
                        {
                            throw new ApplicationException($"'StatusId' in UserConfigurationFile can`t be parsed to int! The value is {statusId}");
                        }

                        var user = new User
                        {
                            UserName = email,
                            Email = email,
                            EmailConfirmed = true,
                            FirstName = firstName,
                            SecondName = secondName,
                            ThirdName = thirdName,
                            StatusId = stId,
                            SignUpTime = DateTime.Now,
                            LastStatusChangeTime = DateTime.Now
                        };

                        if (doctor != null)
                        {
                            string doctorsInfo = item.Doctor.DoctorsInfo;
                            string address = item.Doctor.Address;
                            string isApproved = item.Doctor.IsApproved;
                            string specializationId = item.Doctor.SpecializationId;

                            if(!bool.TryParse(isApproved, out bool isAppr))
                            {
                                throw new ApplicationException($"'IsApproved' in UserConfigurationFile can`t be parsed to bool! The value is {isApproved}");
                            }
                            if (!int.TryParse(specializationId, out int specId))
                            {
                                throw new ApplicationException($"'SpecializationId' in UserConfigurationFile can`t be parsed to int! The value is {isApproved}");
                            }

                            user.Doctor = new Doctor
                            {
                                DoctorsInfo = doctorsInfo,
                                Address = address,
                                IsApproved = isAppr,
                                SpecializationId = specId
                            };
                        }

                        IdentityResult result = await userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
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
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Probably it`s something wrong with parsing jsonFiles: {ex.Message}");
                throw;
            }
        }
    }
}

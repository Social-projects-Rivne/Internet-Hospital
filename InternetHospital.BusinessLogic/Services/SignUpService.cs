using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IRegistrationService _registrationService;
        private readonly IFilesService _uploadingFiles;

        public SignUpService(IRegistrationService registrationService, IFilesService uploadingFiles)
        {
            _registrationService = registrationService;
            _uploadingFiles = uploadingFiles;
        }

        public async Task<(bool state, string text)> CheckSignUpModel(UserManager<User> _userManager,
                                                          UserRegistrationModel userRegistrationModel, IFormFile file)
        {
            bool state = true;
            string text;

            const string PATIENT = "Patient";
            const string DOCTOR = "Doctor";

            if (await _userManager.FindByEmailAsync(userRegistrationModel.Email) == null)
            {
                User user = null;
                switch (userRegistrationModel.Role)
                {
                    case PATIENT:
                        {
                            user = await _registrationService.PatientRegistration(userRegistrationModel);
                            text = user == null ? "Error during patient registration" : "All is fine during patient registration";
                            break;
                        }
                    case DOCTOR:
                        {
                            user = await _registrationService.DoctorRegistration(userRegistrationModel);
                            text = user == null ? "Error during doctor registration" : "All is fine during doctor registration";
                            break;
                        }
                    default:
                        {
                            text = "Role type doesn't match conditions! must be only a Doctor or a patient";
                            break;
                        }
                }
                if(user == null)
                {
                    state = false;
                    return (state, text);
                }

                var userWithAvatar = await _uploadingFiles.UploadAvatar(file, user);

                if (userWithAvatar == null)
                {
                    return (state, "Your account is created. But avatar wasn't upload. Confirm your account on email!");
                }
                else
                {
                    return (state, "Your account is created. Confirm your account on email!");
                }
            }
            else
            {
                state = false;
                return (state,"This email is already registered!");
            }
        }
    }
}

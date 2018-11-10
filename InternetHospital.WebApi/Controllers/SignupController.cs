using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using InternetHospital.BusinessLogic.Interfaces;

namespace InternetHospital.WebApi.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly IRegistrationService _registrationService;
        private readonly IUploadingFiles _uploadingFiles;

        public SignupController(UserManager<User> userManager, IMailService mailService, 
            IRegistrationService registrationService, IUploadingFiles uploadingFiles)
        {
            _userManager = userManager;
            _mailService = mailService;
            _registrationService = registrationService;
            _uploadingFiles = uploadingFiles;
        }

        // POST: api/Signup
        [HttpPost]
        public async Task<ActionResult> Register(IFormFile formFile)
        {
            const string PATIENT = "Patient";
            const string DOCTOR = "Doctor";

            var file = Request.Form.Files["Image"];
            var userRegistrationModel = new UserRegistrationModel
            {
                Email = Request.Form["Email"],
                Password = Request.Form["Password"],
                ConfirmPassword = Request.Form["ConfirmPassword"],
                UserName = Request.Form["Email"],
                Role = Request.Form["Role"]
            };

            TryValidateModel(userRegistrationModel);
            if (userRegistrationModel.UserName.Contains('/'))
            {
                ModelState.AddModelError("", "Invalid email name!");
            }
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(userRegistrationModel.Email) == null)
                {
                    string callbackUrl = null;
                    User user = null;
                    if (userRegistrationModel.Role == PATIENT)
                    {
                        user = await _registrationService.PatientRegistration(userRegistrationModel);
                        if (user == null)
                        {
                            return BadRequest(new { message = "Error during patient registration" });
                        }
                        callbackUrl = await GenerateConfirmationLink(user);
                    } 
                    else if (userRegistrationModel.Role == DOCTOR)
                    {
                        user = await _registrationService.DoctorRegistration(userRegistrationModel);
                        if (user == null)
                        {
                            return BadRequest(new { message = "Error during doctor registration" });
                        }
                        callbackUrl = await GenerateConfirmationLink(user);
                    }
                    else
                    {
                        return BadRequest(new { message = "Role type doesn't match conditions! must be only a Doctor or a patient" });
                    }
                    var userWithAvatar = await _uploadingFiles.UploadAvatar(file, user);
                    await _mailService.SendMsgToEmail(user.Email, "Confirm Your account, please",
                                 $"Confirm registration folowing the link: <a href='{callbackUrl}'>Confirm email NOW</a>");
                    if (userWithAvatar == null)
                    {
                        return Ok(new { message = "Your account is created. But avatar wasn't upload. Confirm your account on email!" });
                    }
                    else
                    {
                        return Ok(new { message = "Your account is created. Confirm your account on email!" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "This email is already registered!" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong form!");
                return BadRequest( new { message = ModelState.Root.Errors[0].ErrorMessage });
            }            
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(new { message = "lost userId or token!" });
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { message = "Not such users in DB to confirm" });
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return Ok(new { message = "Email Confirmed!" });
        }

        private async Task<string> GenerateConfirmationLink(User user)
        {
            var codes = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Signup",
                new { userId = user.Id, code = codes },
                protocol: HttpContext.Request.Scheme
            );
            return callbackUrl;
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.DataAccess;
using Microsoft.AspNetCore.Routing;

namespace InternetHospital.WebApi.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;
        private IMailService _mailService;
        private ApplicationContext _context;
        private IRegistrationService _registrationService;
        private IUploadingFiles _uploadingFiles;

        public SignupController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager,
            IMailService mailService, ApplicationContext context, IRegistrationService registrationService,
            IUploadingFiles uploadingFiles)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _context = context;
            _registrationService = registrationService;
            _uploadingFiles = uploadingFiles;
        }

        // POST: api/Signup
        [HttpPost]
        public async Task<ActionResult> Register(IFormFile formFile)
        {
            var file = Request.Form.Files["Image"];
            var vm = new UserRegistrationModel
            {
                Email = Request.Form["Email"],
                Password = Request.Form["Password"],
                ConfirmPassword = Request.Form["ConfirmPassword"],
                UserName = Request.Form["Email"],
                Role = Request.Form["Role"]
            };

            bool isValid = TryValidateModel(vm);

            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(vm.Email) == null)
                {
                    string callbackUrl = null;
                    User user = null;
                    if (vm.Role == "Patient" || vm.Role == "patient")
                    {
                        user = await _registrationService.PatientRegistration(vm);
                        if (user == null)
                        {
                            return BadRequest("Error during patient registration");
                        }
                        callbackUrl = await GenerateConfirmationLink(user);
                    } 
                    else if (vm.Role == "Doctor" || vm.Role == "doctor")
                    {
                        user = await _registrationService.DoctorRegistration(vm);
                        if (user == null)
                        {
                            return BadRequest("Error during doctor registration");
                        }
                        callbackUrl = await GenerateConfirmationLink(user);
                    }
                    else
                    {
                        return BadRequest("Role type doesn't match conditions! must be only a Doctor or a patient");
                    }
                    var userWithAvatar = await _uploadingFiles.UploadAvatar(file, user);
                    await _mailService.SendMsgToEmail(user.Email, "Confirm Your account, please",
                                 $"Confirm registration folowing the link: <a href='{callbackUrl}'>Confirm email NOW</a>");
                    if (userWithAvatar == null)
                    {
                        return Ok("Your account is created. But avatar didn't upload. Confirm your account on email!");
                    }
                    else
                    {
                        return Ok("Your account is created. Confirm your account on email!");
                    }
                }
                else
                {
                    return BadRequest("This email is already exist!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong form!");
                return BadRequest(ModelState);
            }            
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("lost userId or token!");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Not such users in DB to confirm");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return Ok("Email Confirmed!");
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
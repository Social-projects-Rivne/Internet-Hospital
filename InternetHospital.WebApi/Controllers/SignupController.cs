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
        private readonly ISignUpService _signUpService;
        private readonly IMailService _mailService;

        public SignupController(UserManager<User> userManager, IMailService mailService, ISignUpService signUpService)
        {
            _userManager = userManager;
            _signUpService = signUpService;
            _mailService = mailService;
        }

        // POST: api/Signup
        [HttpPost]
        public async Task<IActionResult> Register(IFormFile formFile)
        {
            string callbackUrl = null;

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
                (bool state, string text) = await _signUpService.CheckSignUpModel(_userManager, userRegistrationModel, file);
                if (state)
                {
                    await _mailService.SendMsgToEmail(userRegistrationModel.Email, "Confirm Your account, please",
            $"Confirm registration folowing the link: <a href='{callbackUrl}'>Confirm email NOW</a>");

                    return Ok(new { message = text });
                }
                else
                {
                    return BadRequest(new { message = text });
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong form!");
                return BadRequest(new { message = ModelState.Root.Errors[0].ErrorMessage });
            }
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId ?? "0");
            if (user == null || code == null)
            {
                return BadRequest(new { message = "Not such users in DB to confirm" });
            }
            else
            {
                var result = await _userManager.ConfirmEmailAsync(user, code);

                return Ok(new { message = "Email Confirmed!" });
            }
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
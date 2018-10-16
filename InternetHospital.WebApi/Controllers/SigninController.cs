using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.WebApi.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InternetHospital.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private ApplicationContext _context;
        private readonly TokenService tokenService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public SigninController(IOptions<AppSettings> appSettings, ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            tokenService = new TokenService(_context, _appSettings, _userManager);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(LoginForm form)
        {
            var user = await _userManager.FindByNameAsync(form.UserName);
            if (user != null)            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return NotFound(new { message = "User not found or not confirmed" });
                }
            }
            if (!await _userManager.CheckPasswordAsync(user, form.Password))
            {
                return NotFound(new { message = "Wrong password" });
            }
            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(await tokenService.GenerateAccessToken(user)),
                refresh_token = (await tokenService.GenerateRefreshToken(user)).Token,
                user_id = user.Id,
                user_email = user.Email
            });

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refresh)
        {
            var refreshedToken = await tokenService.RefreshTokinValidation(refresh);
            if (refreshedToken == null)
            {
                return BadRequest("invalid_grant");
            }
            var user = await _userManager.FindByIdAsync(refreshedToken.UserId.ToString());
            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(await tokenService.GenerateAccessToken(user)),
                refresh_token = refreshedToken.Token,
                user_id = user.Id,
                user_email = user.FirstName
            });
        }       
    }
}
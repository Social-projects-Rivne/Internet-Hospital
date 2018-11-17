using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly ISignInService _signIn;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public SigninController(UserManager<User> userManager,
            ITokenService tokenService,
            ISignInService signIn)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signIn = signIn;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]UserLoginModel form)
        {
            var (user,state) = await _signIn.CheckIfExist(form, _userManager);
            IActionResult status;
            if (state == true)
            {
                status = Ok(new
                {
                    user_avatar = user.AvatarURL,
                    access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessToken(user)),
                    refresh_token = _tokenService.GenerateRefreshToken(user).Token
                });
            }
            else
            {
                status = NotFound(new { message = "You have entered an invalid username or password" });
            }
            return status;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenModel refresh)
        {
            var refreshedToken = _tokenService.RefreshTokenValidation(refresh.RefreshToken);
            if (refreshedToken == null)
            {
                return BadRequest(new { message = "invalid_grant" });
            }
            var user = await _userManager.FindByIdAsync(refreshedToken.UserId.ToString());
            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessToken(user)),
                refresh_token = refreshedToken.Token
            });
        }
    }

}
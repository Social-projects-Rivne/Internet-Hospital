using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.WebApi.Auth
{
    public class TokenService
    {
        private readonly AppSettings _appSettings;
        private ApplicationContext _context;
        private UserManager<User> _userManager;

        public TokenService(ApplicationContext context, AppSettings settings, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appSettings = settings;
            _context = context;
        }

        /// <summary>
        /// Method for generation access tokens for app uesers
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<JwtSecurityToken> GenerateAccessToken(User user)
        {

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, userRoles[0])
               };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var currentTime = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtIssuer,
                notBefore: currentTime,
                claims: claims,
                expires: currentTime.AddMinutes(_appSettings.JwtExpireMinutes),
                signingCredentials: credential);


            return token;

        }


        /// <summary>
        /// Method for generation and saving in DB refresh tokens for renewing access tokens
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            return await Task.Run(() =>
            {
                var usertokens = _context.Tokens.Where(x => x.UserId == user.Id);

                if (usertokens.Count() > 5)
                {
                    foreach (var item in usertokens)
                    {
                        _context.Tokens.Remove(item);
                    }
                    _context.SaveChanges();
                }


                var newRefreshToken = new RefreshToken
                {
                    UserId = user.Id,
                    Token = Guid.NewGuid().ToString(),
                    ExpiresDate = DateTime.UtcNow.AddDays(15),
                    Revoked = false
                };

                _context.Tokens.Add(newRefreshToken);
                _context.SaveChanges();

                return newRefreshToken;
            });
        }


        /// <summary>
        /// Method for validation of refresh token that was sent by user
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<RefreshToken> RefreshTokinValidation(string token)
        {

            return await Task.Run(() =>
            {
                RefreshToken refreshedToken = _context.Tokens.FirstOrDefault(x => x.Token == token);


                if (refreshedToken == null)
                    return null;


                if (refreshedToken.ExpiresDate < DateTime.UtcNow || refreshedToken.Revoked == true)
                {
                    _context.Tokens.Remove(refreshedToken);
                    _context.SaveChanges();
                    return null;
                }


                string newrefreshtoken = Guid.NewGuid().ToString();
                refreshedToken.Token = newrefreshtoken;
                _context.SaveChanges();


                return refreshedToken;
            });
        }
    }
}

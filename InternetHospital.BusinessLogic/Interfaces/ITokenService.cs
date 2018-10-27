using InternetHospital.DataAccess.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(User user);
        RefreshToken RefreshTokenValidation(string token);
    }
}

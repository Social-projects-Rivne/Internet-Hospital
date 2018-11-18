using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class SignInService : ISignInService
    {
        public async Task<(User user, bool state)> CheckIfExist(UserLoginModel model, UserManager<User> userManager)
        {
            var user = await userManager.FindByNameOrEmailAsync(model.UserName);
            if (user == null)
            {
                return (null, false);
            }
            if (await userManager.IsEmailConfirmedAsync(user) && await userManager.CheckPasswordAsync(user, model.Password))
            {
                return (user, true);
            }
            else
            {
                return (user, false);
            }
        }
    }
}

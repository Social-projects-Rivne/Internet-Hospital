using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public static class Extensions
    {
        public static async Task<User> FindByNameOrEmailAsync(this UserManager<User> userManager, string usernameOrEmail)
        {
            var emailValidator = new EmailAddressAttribute();
            if (emailValidator.IsValid(usernameOrEmail))
            {
                return await userManager.FindByEmailAsync(usernameOrEmail);
            }
            return await userManager.FindByNameAsync(usernameOrEmail);
        }
    }
}

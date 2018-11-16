using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface ISignUpService
    {
        Task<(bool state, string text)> CheckSignUpModel(UserManager<User> _userManager,
                                            UserRegistrationModel userRegistrationModel, IFormFile file);
    }
}

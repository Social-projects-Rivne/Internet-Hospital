using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IUserListService
    {
        List<UserModel> GetUsers();
    }
}

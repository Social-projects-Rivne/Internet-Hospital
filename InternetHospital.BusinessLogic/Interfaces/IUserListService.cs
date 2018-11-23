using InternetHospital.BusinessLogic.Models;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IUserListService
    {
        List<UserModel> GetUsers();
        (List<UserModel> users, int count) FilteredUsers(UserSearchParameters queryParameters);
    }
}

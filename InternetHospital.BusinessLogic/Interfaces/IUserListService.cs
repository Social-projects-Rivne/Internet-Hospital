using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IUserListService
    {
        IEnumerable<UserModel> GetUsers();
        (IEnumerable<UserModel> users, int count) FilteredUsers(UserSearchParameters queryParameters);
        IEnumerable<Status> GetStatuses();
    }
}

using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternetHospital.BusinessLogic.Helpers;

namespace InternetHospital.BusinessLogic.Services
{
    public class UserListService : IUserListService
    {
        private readonly ApplicationContext _context;

        public UserListService(ApplicationContext context)
        {
            _context = context;
        }

        public (IEnumerable<UserModel> users, int count) FilteredUsers(UserSearchParameters queryParameters)
        {

            var users = _context.Users.AsQueryable();

            if (queryParameters.SearchByName != null)
            {
                var toLowerSearchParameter = queryParameters.SearchByName.ToLower();
                users = users
                    .Where(x => x.FirstName.ToLower().Contains(toLowerSearchParameter)
                    || x.SecondName.ToLower().Contains(toLowerSearchParameter)
                    || x.ThirdName.ToLower().Contains(toLowerSearchParameter));
            }

            if (queryParameters.SearchByStatus != null)
            {
                users = users.Where(d => d.StatusId == queryParameters.SearchByStatus);
            }

            var usersQuantity = users.Count();
            var usersResult = PaginationHelper<User>
                .GetPageValues(users, queryParameters.PageCount, queryParameters.Page)
                .OrderBy(x => x.SecondName)
                .Select(u => Mapper.Map<User, UserModel>(u))
                .ToList();

            return (usersResult, usersQuantity);

        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _context.Users.Select(u => Mapper.Map<User, UserModel>(u)).ToList();
        }
    }
}

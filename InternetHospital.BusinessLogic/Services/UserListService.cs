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

        private const string SORT_BY_FIRST_NAME = "firstName";
        private const string SORT_BY_SECOND_NAME = "secondName";
        private const string SORT_BY_THIRD_NAME = "thirdName";
        private const string SORT_BY_EMAIL = "email";
        private const string ORDER_ASC = "asc";

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

        public IEnumerable<Status> GetStatuses()
        {
            return _context.Statuses.ToList();
        }

        FilteredModel<UserModel> IUserListService.GetUsers(ModeratorSearchParameters queryParameters)
        {
            //return _context.Users.Select(u => Mapper.Map<User, UserModel>(u)).ToList();
            var users = _context.Users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    ThirdName = u.ThirdName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Status = u.Status.Name
                });

            // check search parameter
            if (!string.IsNullOrEmpty(queryParameters.SearchByName))
            {
                var lowerSearchParam = queryParameters.SearchByName.ToLower();
                users = users.Where(u => u.FirstName.ToLower().Contains(lowerSearchParam)
                                                   || u.SecondName.ToLower().Contains(lowerSearchParam)
                                                   || u.ThirdName.ToLower().Contains(lowerSearchParam));
            }

            // check order parameter
            if (!string.IsNullOrEmpty(queryParameters.Order))
            {
                users = SortUsers(users, queryParameters.Sort, queryParameters.Order);
            }

            // declare new FilteredModel
            FilteredModel<UserModel> fModel = new FilteredModel<UserModel>();
            fModel.Amount = users.Count();

            users = PaginationHelper<UserModel>
                .GetPageValues(users, queryParameters.PageSize, queryParameters.Page);

            fModel.Results = users.ToList();

            return fModel;
        }


        private IQueryable<UserModel> SortUsers(IQueryable<UserModel> users, string sortField, string orderBy)
        {
            if (orderBy == ORDER_ASC)
            {
                switch (sortField)
                {
                    case SORT_BY_EMAIL:
                        users = users.OrderBy(u => u.Email);
                        break;
                    case SORT_BY_FIRST_NAME:
                        users = users.OrderBy(u => u.FirstName);
                        break;
                    case SORT_BY_SECOND_NAME:
                        users = users.OrderBy(u => u.SecondName);
                        break;
                    case SORT_BY_THIRD_NAME:
                        users = users.OrderBy(u => u.ThirdName);
                        break;
                }
            }
            else
            {
                switch (sortField)
                {
                    case SORT_BY_EMAIL:
                        users = users.OrderByDescending(u => u.Email);
                        break;
                    case SORT_BY_FIRST_NAME:
                        users = users.OrderByDescending(u => u.FirstName);
                        break;
                    case SORT_BY_SECOND_NAME:
                        users = users.OrderByDescending(u => u.SecondName);
                        break;
                    case SORT_BY_THIRD_NAME:
                        users = users.OrderByDescending(u => u.ThirdName);
                        break;
                }
            }
            return users;
        }
    }
}

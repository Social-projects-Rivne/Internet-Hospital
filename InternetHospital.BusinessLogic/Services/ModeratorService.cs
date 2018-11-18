using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetHospital.BusinessLogic.Services
{
    public class ModeratorService: IModeratorService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        private const string MODERATOR = "Moderator";
        private const string ACTIVE_STATUS = "Active";
        private const string SORT_BY_FIRST_NAME = "firstName";
        private const string SORT_BY_SECOND_NAME = "secondName";
        private const string SORT_BY_THIRD_NAME = "thirdName";
        private const string SORT_BY_STATUS = "status";
        private const string SORT_BY_SIGN_UP = "signUpTime";
        private const string SORT_BY_EMAIL = "email";

        private const string ORDER_ASC = "asc";


        public ModeratorService(ApplicationContext context, UserManager<User> um,
            RoleManager<IdentityRole<int>> rm)
        {
            _context = context;
            _userManager = um;
            _roleManager = rm;
        }

        public async Task<(User, string)> CreateModeratorAsync(ModeratorCreatingModel vm)
        {
            if (await _userManager.FindByEmailAsync(vm.Email) == null)
            {
                var user = Mapper.Map<ModeratorCreatingModel, User>(vm, cnf => 
                    cnf.AfterMap((src, dest) =>
                    {
                        dest.StatusId = _context.Statuses.FirstOrDefault(s => s.Name == ACTIVE_STATUS).Id;
                        dest.SignUpTime = DateTime.UtcNow;
                        dest.UserName = src.Email;
                    }));
                if (await _roleManager.FindByNameAsync(MODERATOR) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>(MODERATOR));
                }
                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, MODERATOR);
                    return (user, "Confirm link was send on email!");
                }

                return (null, "Error during registration");
            }
            return (null, "User with such email is already created!");
        }

        public FilteredModeratorsModel GetFilteredModerators(ModeratorSearchParameters queryParameters)
        {
            var moderators = GetModerators();

            if (!queryParameters.IncludeAll)
            {
                moderators = moderators.Where(m => m.Status.Name == ACTIVE_STATUS);
            }

            if (!string.IsNullOrEmpty(queryParameters.SearchByName))
            {
                var lowerSearchParam = queryParameters.SearchByName.ToLower();
                moderators = moderators.Where(m => m.FirstName.ToLower().Contains(lowerSearchParam)
                                                   || m.SecondName.ToLower().Contains(lowerSearchParam)
                                                   || m.ThirdName.ToLower().Contains(lowerSearchParam));
            }
            FilteredModeratorsModel fModel = new FilteredModeratorsModel();
            fModel.AmountOfAllFiltered = moderators.Count();
            fModel.Moderators = Pagination(moderators, queryParameters.Page, queryParameters.PageSize).ToList();
            return fModel;
        }

        private IQueryable<User> GetModerators()
        {
            var moderatorRoleId = _roleManager.Roles.FirstOrDefault(r => r.Name == MODERATOR).Id;

            var modersIds = _context.UserRoles.Where(r => r.RoleId == moderatorRoleId).Select(r => r.UserId);

            var moderators = _userManager.Users.Where(u => modersIds.Contains(u.Id));
            return moderators;
        }

        private IQueryable<ModeratorModel> Pagination(IQueryable<User> moderators, int pageNumber, int elementsOnPage)
        {
            var moders = moderators.Skip((pageNumber - 1) * elementsOnPage)
                                    .Take(elementsOnPage)
                                    .Select(m => new ModeratorModel
                                    {
                                        Id = m.Id,
                                        Email = m.Email,
                                        FirstName = m.FirstName,
                                        SecondName = m.SecondName,
                                        ThirdName = m.ThirdName,
                                        SignUpTime = m.SignUpTime,
                                        Status = _context.Statuses.FirstOrDefault(s => s.Id == m.StatusId).Name
                                    });
            return moders;
        }

        private IQueryable<User> SortModerators(IQueryable<User> moderators, string sortField, string orderBy)
        {
            if (orderBy == ORDER_ASC)
            {
                switch (sortField)
                {
                    case SORT_BY_EMAIL:
                        moderators = moderators.OrderBy(m => m.Email);
                        break;
                    case SORT_BY_FIRST_NAME:
                        moderators = moderators.OrderBy(m => m.FirstName);
                        break;
                    case SORT_BY_SECOND_NAME:
                        moderators = moderators.OrderBy(m => m.SecondName);
                        break;
                    case SORT_BY_THIRD_NAME:
                        moderators = moderators.OrderBy(m => m.ThirdName);
                        break;
                    case SORT_BY_SIGN_UP:
                        moderators = moderators.OrderBy(m => m.SignUpTime);
                        break;
                    case SORT_BY_STATUS:
                        moderators = moderators.OrderBy(m => m.Status.Name);
                        break;
                }
            }
            else
            {
                switch (sortField)
                {
                    case SORT_BY_EMAIL:
                        moderators = moderators.OrderByDescending(m => m.Email);
                        break;
                    case SORT_BY_FIRST_NAME:
                        moderators = moderators.OrderByDescending(m => m.FirstName);
                        break;
                    case SORT_BY_SECOND_NAME:
                        moderators = moderators.OrderByDescending(m => m.SecondName);
                        break;
                    case SORT_BY_THIRD_NAME:
                        moderators = moderators.OrderByDescending(m => m.ThirdName);
                        break;
                    case SORT_BY_SIGN_UP:
                        moderators = moderators.OrderByDescending(m => m.SignUpTime);
                        break;
                    case SORT_BY_STATUS:
                        moderators = moderators.OrderByDescending(m => m.Status.Name);
                        break;
                }
            }
            return moderators;
        }
    }
}

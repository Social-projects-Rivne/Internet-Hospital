using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace InternetHospital.BusinessLogic.Services
{
    public class UserListService : IUserListService
    {
        private readonly ApplicationContext _context;

        public UserListService(ApplicationContext context)
        {
            _context = context;
        }

        public List<UserModel> GetUsers()
        {
            return _context.Users.Select(u => Mapper.Map<User, UserModel>(u)).ToList();
        }
    }
}

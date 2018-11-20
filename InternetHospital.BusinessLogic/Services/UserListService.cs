using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class UserListService : IUserListService
    {
        private readonly ApplicationContext _context;

        public UserListService(ApplicationContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}

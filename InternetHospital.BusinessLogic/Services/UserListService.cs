using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace InternetHospital.BusinessLogic.Services
{
    public class UserListService : IUserListService
    {

        private readonly ApplicationContext _context;
        private readonly ILogger<UserModel> _logger;

        public UserListService(ApplicationContext context,
                               ILogger<UserModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public UserModel ChangeUser(UserModel TargetModel, UserModel NewModel)
        {
            var user = _context.Users.Find(TargetModel);
            if (user != null)
            {
                user.FirstName = NewModel.FirstName;
                user.SecondName = NewModel.SecondName;
                user.ThirdName = NewModel.ThirdName;
                user.BirthDate = NewModel.BirthDate;
                user.StatusId = NewModel.StatusId;
                user.AvatarURL = NewModel.AvatarURL;

                try
                {
                    _context.SaveChanges();
                    return NewModel;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"{nameof(UserListService)} failed with {ex.Message}", ex);
                    return null;
                }
            }

            return null;

        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}

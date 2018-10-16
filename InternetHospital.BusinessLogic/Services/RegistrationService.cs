using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.services
{
    public class RegistrationService : IRegistrationService
    {
        private ApplicationContext _context;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public RegistrationService(ApplicationContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<User> DoctorRegistration(UserRegistrationModel vm)
        {
            var user = await UserRegistration(vm);
            if (user == null)
            {
                return null;
            }
            await _userManager.AddToRoleAsync(user, vm.Role);

            var doctor = new Doctor
            {
                IsApproved = false
            };
            doctor.User = user;

            _context.Add(doctor);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> PatientRegistration(UserRegistrationModel vm)
        {
            var user = await UserRegistration(vm);
            if (user == null)
            {
                return null;
            }
            await _userManager.AddToRoleAsync(user, vm.Role);
            return user;            
        }

        private async Task<User> UserRegistration(UserRegistrationModel vm)
        {
            var user = Mapper.Map<User>(vm);
            if (await _roleManager.FindByNameAsync(vm.Role) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(vm.Role));
            }

            if (_context.Statuses.Count() == 0)
                user.Status = new Status { Name = "Undetermined status", Id = 2 };
            user.StatusId = 2; // 2 - New User status

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }
    }
}

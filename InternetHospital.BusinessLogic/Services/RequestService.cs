using AutoMapper;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.EditProfile;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class RequestService : IRequestService
    {
        private const string PATIENT = "Patient";
        private const string DOCTOR = "Doctor";
        private const string DIPLOMA = "Diploma";
        private const string LICENSE = "License";
        private const int APPROVED = 3;
        const int BECOME_A_DOCTOR_REQUEST_ID = 2;

        private ApplicationContext _context;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;
        private IMailService _mailService;

        public RequestService(ApplicationContext context, UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager, IMailService mailService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }



        public PageModel<IEnumerable<ChangeRoleModel>> GetRequestsPatientToDoctor(PageConfig pageConfig) // pass model PAgination model with pageCountModel and pageNumber
        {
            var tmpUsers = _context.TemporaryUsers
                    .Include(tu => tu.User)
                    .Where(tu => tu.isRejected == false && tu.Role == PATIENT
                    && tu.UserRequestTypeId == BECOME_A_DOCTOR_REQUEST_ID
                    && tu.User.StatusId == APPROVED);

            if (tmpUsers.Count() == 0)
            {
                return null;
            }

            var uniqueTmpUsers = tmpUsers
                .Include(u => u.User.Diplomas)
                .Include(u => u.User.Licenses)
                .GroupBy(tu => tu.UserId)
                .Select(tu => tu.LastOrDefault())
                .AsEnumerable();

            var amount = uniqueTmpUsers.Count();
            var patientsRequest = PaginationHelper<TemporaryUser>.GetPageValues(uniqueTmpUsers.AsQueryable(), pageConfig.PageSize, pageConfig.PageIndex);

            var patientsToDoctorModel = patientsRequest.AsEnumerable()
                .Select(u => new ChangeRoleModel
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    ThirdName = u.ThirdName,
                    Email = u.User.Email,
                    Address = u.Address,
                    Specialization = u.Specialization,
                    PhoneNumber = u.PhoneNumber,
                    RequestTime = u.AddedTime,
                    Diplomas = u.User.Diplomas
                                    .Where(d => d.AddedTime == u.AddedTime)
                                    .Select(d => d.DiplomaURL).ToList(),
                    License = u.User.Licenses
                                    .FirstOrDefault(l => l.AddedTime == u.AddedTime).LicenseURL
                });

            var pageModel = new PageModel<IEnumerable<ChangeRoleModel>>
            {
                EntityAmount = amount,
                Entities = patientsToDoctorModel
            };
            return pageModel;
        }

        public async Task<(bool, string)> HandlePatientToDoctorRequest(int id, bool isApproved)
        {
            var selectedUserTmps = _context.TemporaryUsers
                    .Include(tu => tu.User)
                    .Where(tu => tu.UserId == id && tu.isRejected == false
                    && tu.UserRequestTypeId == BECOME_A_DOCTOR_REQUEST_ID
                    && tu.User.StatusId == APPROVED);

            if (selectedUserTmps.Count() == 0)
            {
                return (false, "User undefined or already handled!");
            }

            // Change state to checked:
            selectedUserTmps.ToList().ForEach(tu => tu.isRejected = true);

            if (isApproved == true)
            {
                var tmpUser = selectedUserTmps.AsEnumerable().LastOrDefault();
                var specializationId = _context.Specializations
                    .FirstOrDefault(s => s.Name == tmpUser.Specialization).Id;

                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                await _userManager.RemoveFromRoleAsync(user, PATIENT);
                await _userManager.AddToRoleAsync(user, DOCTOR);

                var doctor = new Doctor
                {
                    IsApproved = true,
                    User = user,
                    Address = tmpUser.Address,
                    SpecializationId = specializationId
                };
                _context.Add(doctor);
                user.LastStatusChangeTime = DateTime.Now;

                await _mailService.SendMsgToEmail(user.Email, "Request to become a doctor",
                    "Your request to become a doctor was successfully approved!");

                await _context.SaveChangesAsync();
                return (true, "User have been successfully approved!");
            }
            else if (isApproved == false)
            {
                await _context.SaveChangesAsync();
                return (true, "User's request is rejected!");
            }

            await _context.SaveChangesAsync();

            return (false, "Error!");
        }
        public PageModel<IEnumerable<UserEditProfile>> GetRequestsEditProfiel(PageConfig pageConfig)
        {
            var tmpUsers = _context.TemporaryUsers
                    .Include(tu => tu.User)
                    .Where(tu => tu.isRejected == false && tu.UserRequestTypeId == 1);
            if (tmpUsers.Count() == 0)
            {
                return null;
            }

            var uniqueTmpUsers = tmpUsers
                .Include(u => u.User.Passports)
                .Include(u => u.User.Diplomas)
                .Include(u => u.User.Licenses)
                .GroupBy(tu => tu.UserId)
                .Select(tu => tu.LastOrDefault())
                .AsEnumerable();

            var amount = uniqueTmpUsers.Count();
            var usersRequest = PaginationHelper<TemporaryUser>.GetPageValues(uniqueTmpUsers.AsQueryable(), pageConfig.PageSize, pageConfig.PageIndex);
            var UsersEditProfile = usersRequest.AsEnumerable()
                .Select(u => new UserEditProfile
                {
                    Id = u.UserId,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    ThirdName = u.ThirdName,
                    BirthDate = u.BirthDate,
                    Email = u.User.Email,
                    Address = u.Address,
                    Specialization = u.Specialization,
                    PhoneNumber = u.PhoneNumber,
                    RequestTime = u.AddedTime,
                    Diplomas = u.User.Diplomas?.Where(d => d.AddedTime == u.AddedTime)
                                    .Select(d => d.DiplomaURL).ToList(),
                    Passport = u.User.Passports?.Where(p => p.AddedTime == u.AddedTime)
                                    .Select(p => p.PassportURL).ToList(),
                    License = u.User.Licenses?
                                    .FirstOrDefault(l => l.AddedTime == u.AddedTime)?.LicenseURL
                });
            var pageModel = new PageModel<IEnumerable<UserEditProfile>>
            {
                EntityAmount = amount,
                Entities = UsersEditProfile
            };
            return pageModel;
        }
        public async Task<(bool, string)> HandleEditUserProfile(int id, bool isApproved)
        {
            var tmpUsers = _context.TemporaryUsers
                    .Include(tu => tu.User)
                    .Where(tu => tu.isRejected == false && tu.UserId == id && tu.UserRequestTypeId == 1);

            if (tmpUsers.Count() == 0)
            {
                return (false, "User undefined or already handled!");
            }

            tmpUsers.ToList().ForEach(tu => tu.isRejected = true);

            if (isApproved)
            {
                var tmpUser = tmpUsers.AsEnumerable().LastOrDefault();
                var user = _context.Users.Include(u => u.Doctor)
                        .FirstOrDefault(u => u.Id == id);
                if (tmpUser.User.Doctor == null)
                {
                    Mapper.Map<TemporaryUser, User>(tmpUser, user,
                        config => config.AfterMap((src, dest) =>
                        {
                            dest.StatusId = 3;
                        }));

                    user.LastStatusChangeTime = DateTime.Now;
                    _context.Users.Update(user);

                    await _mailService.SendMsgToEmail(user.Email, "Request to edit profile",
                        "Your request to edit profile was successfully approved!");

                    await _context.SaveChangesAsync();
                    return (true, "Successfully handled!");
                }
                else if (tmpUser.User.Doctor != null)
                {
                    var specializationId = _context.Specializations
                        .FirstOrDefault(s => s.Name == tmpUser.Specialization).Id;

                    Mapper.Map<TemporaryUser, User>(tmpUser, user,
                        config => config.AfterMap((src, dest) =>
                        {
                            dest.Doctor.Address = src.Address;
                            dest.Doctor.SpecializationId = specializationId;
                            dest.StatusId = 3;
                        }));
                    user.LastStatusChangeTime = DateTime.Now;
                    _context.Users.Update(user);

                    await _mailService.SendMsgToEmail(user.Email, "Request to edit profile",
                        "Your request to edit profile was successfully approved!");

                    await _context.SaveChangesAsync();

                    return (true, "Successfully handled!");
                }
            }
            else if (isApproved == false)
            {
                await _context.SaveChangesAsync();
                return (true, "User's request is rejected!");
            }

            await _context.SaveChangesAsync();
            return (false, "Error!");
        }
    }
}


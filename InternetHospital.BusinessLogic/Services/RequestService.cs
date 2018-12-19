using AutoMapper;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
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
            // int PageCount  // number of users on the page
            // int Page  // page number 
            var tmpUsers = _context.TemporaryUsers
                    .Include(tu => tu.User)
                    .Where(tu => tu.isRejected == false && tu.Role == PATIENT
                    && tu.Specialization != null && tu.User.StatusId == APPROVED);
                    //.Skip(pageConfig.PageSize * (pageConfig.PageIndex - 1))
                    //.Take(pageConfig.PageSize);
            //.GroupBy(tu => tu.UserId)
            //.Select(tu => tu.Last());

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
            // put result in PageModel<IEnumerable<ChangeRoleModel>> and return it
            return pageModel;
        }

        // DON'T NEED it !!!!!!!
        public ChangeRoleModel GetDetailedUserRequest(int id) // or + add addedTime
        {
            var selectedUser = _context.TemporaryUsers
                .Include(tu => tu.User)
                .Include(tu => tu.User.Diplomas)
                .Include(tu => tu.User.Licenses)
                .Where(tu => tu.UserId == id && tu.isRejected == false
                && tu.Specialization != null)
                .AsEnumerable().LastOrDefault();

            if (selectedUser == null)
            {
                return null;
            }
            var patientToDoctorModel = Mapper.Map<TemporaryUser, ChangeRoleModel>(selectedUser,
                config => config.AfterMap((src, dest) =>
                {
                    dest.Email = src.User.Email;
                    dest.Diplomas = src.User.Diplomas
                                        .Where(d => d.AddedTime == src.AddedTime)
                                        .Select(d => d.DiplomaURL).ToList();
                    dest.License = src.User.Licenses
                                    .FirstOrDefault(l => l.AddedTime == src.AddedTime).LicenseURL;
                }));

            return patientToDoctorModel;
        }
        // --------------



        public async Task<(bool, string)> HandlePatientToDoctorRequest(int id, bool isApproved)
        {
            var selectedUserTmps = _context.TemporaryUsers
                    .Include(tu => tu.User)
                    .Where(tu => tu.UserId == id && tu.isRejected == false
                    && tu.Specialization != null && tu.User.StatusId == APPROVED);

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
                return (true, "Successfully handled!");
            }
            else if (isApproved == false)
            {
                await _context.SaveChangesAsync();
                return (true, "User's request is rejected!");
            }

            await _context.SaveChangesAsync();

            return (false, "Error!"); // or change to (true, "You've already handled this user"
        }
    }
}

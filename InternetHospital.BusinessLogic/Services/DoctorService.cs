using System.Collections.Generic;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace InternetHospital.BusinessLogic.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IFilesService _uploadingFiles;
        const string DOCTOR = "Doctor";

        public DoctorService(ApplicationContext context, UserManager<User> userManager, IFilesService uploadingFiles)
        {
            _context = context;
            _userManager = userManager;
            _uploadingFiles = uploadingFiles;
        }

        public DoctorDetailedModel Get(int id)
        {
            DoctorDetailedModel returnedDoctor = null;
            var searchedDoctor = _context.Doctors.Include(d => d.User)
                                                 .Include(d => d.Specialization)
                                                 .Include(d => d.Diplomas)
                                                     .FirstOrDefault(d => d.UserId == id && d.User != null);
            if (searchedDoctor != null && searchedDoctor.IsApproved == true)
            {
                returnedDoctor = new DoctorDetailedModel
                {
                    Id = searchedDoctor.UserId,
                    FirstName = searchedDoctor.User.FirstName,
                    SecondName = searchedDoctor.User.SecondName,
                    ThirdName = searchedDoctor.User.ThirdName,
                    PhoneNumber = searchedDoctor.User.PhoneNumber,
                    BirthDate = searchedDoctor.User.BirthDate,
                    Address = searchedDoctor.Address,
                    Specialization = searchedDoctor.Specialization.Name,
                    DoctorsInfo = searchedDoctor.DoctorsInfo,
                    AvatarURL = searchedDoctor.User.AvatarURL,
                    LicenseURL = searchedDoctor.LicenseURL,
                    DiplomasURL = searchedDoctor.Diplomas.Where(d => d.IsValid == true)
                                                             .Select(d => d.DiplomaURL).ToArray()
                };
            }
            return returnedDoctor;
        }

        public (IEnumerable<DoctorModel> doctors, int count) GetFilteredDoctors(DoctorSearchParameters queryParameters)
        {
            var doctors = _context.Doctors.Where(d => d.IsApproved == true).AsQueryable();

            if (queryParameters.SearchByName != null)
            {
                var toLowerSearchParameter = queryParameters.SearchByName.ToLower();
                doctors = doctors
                    .Where(d => d.User.FirstName.ToLower().Contains(toLowerSearchParameter)
                    || d.User.SecondName.ToLower().Contains(toLowerSearchParameter)
                    || d.User.ThirdName.ToLower().Contains(toLowerSearchParameter));
            }

            if (queryParameters.SearchBySpecialization != null)
            {
                doctors = doctors.Where(d => d.SpecializationId == queryParameters.SearchBySpecialization);
            }

            var doctorsAmount = doctors.Count();
            var doctorsResult = PaginationHelper(doctors, queryParameters.PageCount, queryParameters.Page)
                .OrderBy(x => x.SecondName)
                .ToList(); ;

            return (doctorsResult, doctorsAmount);
        }

        public IEnumerable<SpecializationModel> GetAvailableSpecialization()
        {
            var specializations = _context.Specializations
                .Where(s => s.Doctors.Count > 0)
                .OrderBy(s => s)
                .Select(s => new SpecializationModel
                {
                    Id = s.Id,
                    Specialization = s.Name
                }).ToList();

            return specializations;
        }

        private IQueryable<DoctorModel> PaginationHelper(IQueryable<Doctor> doctors, int pageCount, int page)
        {
            var doctorsModel = doctors
                .Skip(pageCount * (page - 1))
                .Take(pageCount).Select(x => new DoctorModel
                {
                    Id = x.UserId,
                    FirstName = x.User.FirstName,
                    SecondName = x.User.SecondName,
                    ThirdName = x.User.ThirdName,
                    AvatarURL = x.User.AvatarURL,
                    Specialization = x.Specialization.Name
                });

            return doctorsModel;
        }

        public DoctorProfileModel GetDoctorProfile(int userId)
        {
            var user = _context.Users
                .Include(u => u.Doctor)
                .Include(u => u.Doctor.Specialization)
                .FirstOrDefault(u => u.Id == userId);

            var doctorModel = new DoctorProfileModel(); 
                
            Mapper.Map(user, doctorModel, opt => opt.AfterMap((u, dm) =>
            {
                dm.Address = u.Doctor.Address;
                dm.Specialization = u.Doctor.Specialization.Name;
            }));
            
            return doctorModel;
        }

        public async Task<bool> UpdateDoctorInfo(DoctorProfileModel doctorModel, int userId, IFormFileCollection passport, IFormFileCollection diploma, IFormFileCollection license)
        {
            var addedTime = DateTime.Now;
            var doctor = _context.Users
                .Include(u => u.Doctor)
                .FirstOrDefault(u => u.Id == userId);
            if (doctor == null)
            {
                return false;
            }

            var temporaryUser = Mapper.Map<DoctorProfileModel, TemporaryUser>(doctorModel, 
            config => config.AfterMap((src, dest) => 
            {
                dest.AddedTime = addedTime;
                dest.Role = DOCTOR;
                dest.UserId = doctor.Id;

            }));

            _context.Add(temporaryUser);
            _context.Update(doctor);

            await _uploadingFiles.UploadPassport(passport, doctor, addedTime);
            await _uploadingFiles.UploadDiploma(diploma, doctor, addedTime);
            await _uploadingFiles.UploadLicense(license, doctor, addedTime);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

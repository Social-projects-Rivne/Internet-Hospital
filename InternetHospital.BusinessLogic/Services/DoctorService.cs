using System.Collections.Generic;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Models.Appointment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.BusinessLogic.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IFilesService _uploadingFiles;
        const string DOCTOR = "Doctor";
        private const string SORT_BY_FIRST_NAME = "firstName";
        private const string SORT_BY_SECOND_NAME = "secondName";
        private const string SORT_BY_THIRD_NAME = "thirdName";
        private const string SORT_BY_EMAIL = "email";
        private const string ORDER_ASC = "asc";

        public DoctorService(ApplicationContext context, IFilesService uploadingFiles, UserManager<User> userManager)
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
            var doctorsResult = PaginationHelper<Doctor>
                .GetPageValues(doctors, queryParameters.PageCount, queryParameters.Page)
                .Select(x => new DoctorModel
                {
                    Id = x.UserId,
                    FirstName = x.User.FirstName,
                    SecondName = x.User.SecondName,
                    ThirdName = x.User.ThirdName,
                    AvatarURL = x.User.AvatarURL,
                    Specialization = x.Specialization.Name
                })
                .OrderBy(x => x.SecondName)
                .ToList();

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

        public async Task<bool> UpdateDoctorAvatar(string doctorId, IFormFile file)
        {
            if (doctorId != null && file != null)
            {
                var doctor = await _userManager.FindByIdAsync(doctorId);
                await _uploadingFiles.UploadAvatar(file, doctor);
                return true;
            }
            return false;
        }
        public async Task<string> GetDoctorAvatar(string doctorId)
        {
            if (doctorId != null)
            {
                var doctor = await _userManager.FindByIdAsync(doctorId);
                return doctor.AvatarURL;
            }
            else
            {
                return null;
            }
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

        /// <summary>
        /// Add illness data to DB
        /// </summary>
        /// <param name="illnessModel"></param>
        /// <returns>Operation succeed status</returns>
        public (bool status, string message) FillIllnessHistory(IllnessHistoryModel illnessModel)
        {
            bool status = false;
            string message = null;

            var appointment = _context.
                                Appointments
                                .Where(a => a.Id == illnessModel.AppointmentId)
                                .Single();

            if (FillIllness(illnessModel, appointment))
            {
                appointment.StatusId = (int)AppointmentStatuses.FINISHED_STATUS;
                _context.SaveChanges();
                status = true;
            }
            else
            {
                message = "Something wrong with finishing appointment!";
            }

            return (status, message);
        }

        private bool FillIllness(IllnessHistoryModel illnessModel, Appointment appointment)
        {
            bool result = true;
            try
            {
                var illnessHistory = Mapper.Map<IllnessHistoryModel, IllnessHistory>(illnessModel, opt =>
                                                                            opt.AfterMap((im, ih) =>
                                                                            {
                                                                                ih.DoctorId = appointment.DoctorId;
                                                                                ih.UserId = appointment.UserId ?? default;
                                                                                ih.ConclusionTime = DateTime.Now;
                                                                            }));
                _context.IllnessHistories.Add(illnessHistory);
            }
            catch
            {
                result = false;
            }
            return result;
        }
































        /// <summary>
        /// Get all patients who had appointments for current doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="queryParameters"></param>
        /// <returns>
        /// returns a collection of doctor's patients
        /// </returns>
        public FilteredModel<MyPatientModel> GetMyPatients(int doctorId, MyPatientsSearchParameters queryParameters)
        {
            // get all patients of current doctor
            var patients = _context.Users.Where(x => x.Appointments.Any(a => a.DoctorId == doctorId))
                .Select(p => new User
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    SecondName = p.SecondName,
                    ThirdName = p.ThirdName,
                    Email = p.Email
                });

            // check search parameter
            if (!string.IsNullOrEmpty(queryParameters.SearchByName))
            {
                var lowerSearchParam = queryParameters.SearchByName.ToLower();
                patients = patients.Where(m => m.FirstName.ToLower().Contains(lowerSearchParam)
                                                   || m.SecondName.ToLower().Contains(lowerSearchParam)
                                                   || m.ThirdName.ToLower().Contains(lowerSearchParam));
            }

            // check order parameter
            if (!string.IsNullOrEmpty(queryParameters.Order))
            {
                patients = SortMyPatients(patients, queryParameters.Sort, queryParameters.Order);
            }

            // declare new FilteredModel
            FilteredModel<MyPatientModel> fModel = new FilteredModel<MyPatientModel>();
            fModel.Amount = patients.Count();

            patients = PaginationHelper<User>
                .GetPageValues(patients, queryParameters.PageSize, queryParameters.Page);

            fModel.Results = patients.Select(p => new MyPatientModel
            {
                Id = p.Id,
                PatientEmail = p.Email,
                PatientFirstName = p.FirstName,
                PatientSecondName = p.SecondName,
                PatientThirdName = p.ThirdName,
            }).ToList();

            return fModel;
        }

        /// <summary>
        /// Sorting patients by passed parameter
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="sortField"></param>
        /// <param name="orderBy"></param>
        /// <returns>
        /// returns a collection of sorting patients
        /// </returns>
        private IQueryable<User> SortMyPatients(IQueryable<User> patients, string sortField, string orderBy)
        {
            if (orderBy == ORDER_ASC)
            {
                switch (sortField)
                {
                    case SORT_BY_EMAIL:
                        patients = patients.OrderBy(p => p.Email);
                        break;
                    case SORT_BY_FIRST_NAME:
                        patients = patients.OrderBy(p => p.FirstName);
                        break;
                    case SORT_BY_SECOND_NAME:
                        patients = patients.OrderBy(p => p.SecondName);
                        break;
                    case SORT_BY_THIRD_NAME:
                        patients = patients.OrderBy(p => p.ThirdName);
                        break;
                }
            }
            else
            {
                switch (sortField)
                {
                    case SORT_BY_EMAIL:
                        patients = patients.OrderByDescending(p => p.Email);
                        break;
                    case SORT_BY_FIRST_NAME:
                        patients = patients.OrderByDescending(p => p.FirstName);
                        break;
                    case SORT_BY_SECOND_NAME:
                        patients = patients.OrderByDescending(p => p.SecondName);
                        break;
                    case SORT_BY_THIRD_NAME:
                        patients = patients.OrderByDescending(p => p.ThirdName);
                        break;
                }
            }
            return patients;
        }
    }
}

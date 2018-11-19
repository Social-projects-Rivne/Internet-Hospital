using System.Collections.Generic;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using InternetHospital.BusinessLogic.Helpers;

namespace InternetHospital.BusinessLogic.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationContext _context;

        public DoctorService(ApplicationContext context)
        {
            _context = context;
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
    }
}

using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class DoctorService : IDoctorService
    {
        private ApplicationContext _context;

        public DoctorService(ApplicationContext context)
        {
            _context = context;
        }

        public (IQueryable<DoctorsDto> doctors, int count) GetAll(DoctorSearchParameters queryParameters)
        {
            var _doctors = _context.Doctors.AsQueryable();
            if (queryParameters.SearchByName != null)
            {
                _doctors = _doctors
                    .Where(x => x.User.FirstName.ToLower().Contains(queryParameters.SearchByName.ToLower())
                    || x.User.SecondName.ToLower().Contains(queryParameters.SearchByName.ToLower())
                    || x.User.ThirdName.ToLower().Contains(queryParameters.SearchByName.ToLower()));
            }
            if (queryParameters.SearchBySpecialization != null)
            {
                _doctors = _doctors.Where(x => x.SpecializationId == queryParameters.SearchBySpecialization);
            }
            int doctorsAmount = _doctors.Count();
            var doctorsResult = _doctors
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount).Select(x => new DoctorsDto
                {
                    Id = x.UserId,
                    FirstName = x.User.FirstName,
                    SecondName = x.User.SecondName,
                    ThirdName = x.User.ThirdName,
                    AvatarURL = x.User.AvatarURL,
                    Specialization = x.Specialization.Name
                });
            return (doctorsResult, doctorsAmount);
        }

    }
}

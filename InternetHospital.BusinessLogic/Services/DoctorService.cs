using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationContext _context;

        public DoctorService(ApplicationContext context)
        {
            _context = context;
        }

        public (IQueryable<DoctorModel> doctors, int count) GetAll(DoctorSearchParameters queryParameters)
        {
            var doctors = _context.Doctors.AsQueryable();

            if (queryParameters.SearchByName != null)
            {
                var toLowerSearchParameter = queryParameters.SearchByName.ToLower();
                doctors = doctors
                    .Where(x => x.User.FirstName.ToLower().Contains(toLowerSearchParameter)
                    || x.User.SecondName.ToLower().Contains(toLowerSearchParameter)
                    || x.User.ThirdName.ToLower().Contains(toLowerSearchParameter));
            }

            if (queryParameters.SearchBySpecialization != null)
            {
                doctors = doctors.Where(x => x.SpecializationId == queryParameters.SearchBySpecialization);
            }

            int doctorsAmount = doctors.Count();
            var doctorsResult = PaginationHelper(doctors, queryParameters.PageCount, queryParameters.Page);

            return (doctorsResult, doctorsAmount);
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

    }
}

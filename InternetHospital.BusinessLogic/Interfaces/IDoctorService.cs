using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IDoctorService
    {
        DoctorDetailedModel Get(int id);

        (IEnumerable<DoctorModel> doctors, int count) GetFilteredDoctors(DoctorSearchParameters queryParameters);
        IEnumerable<SpecializationModel> GetAvailableSpecialization();
        Task<bool> UpdateDoctorInfo(DoctorProfileModel doctorModel, int userId, IFormFileCollection passport,
            IFormFileCollection diploma, IFormFileCollection license);
        DoctorProfileModel GetDoctorProfile(int userId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;
using Microsoft.AspNetCore.Http;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IDoctorService
    {
        DoctorDetailedModel Get(int id);

        (IEnumerable<DoctorModel> doctors, int count) GetFilteredDoctors(DoctorSearchParameters queryParameters);
        IEnumerable<SpecializationModel> GetAvailableSpecialization();
        Task<bool> UpdateDoctorInfo(DoctorProfileModel doctorModel, int userId,
            IFormFileCollection passport, IFormFileCollection diploma, IFormFileCollection license);
        DoctorProfileModel GetDoctorProfile(int userId);
        Task<bool> UpdateDoctorAvatar(string doctorId, IFormFile file);
        Task<string> GetDoctorAvatar(string doctorId);
        (bool status, string message) FillIllnessHistory(IllnessHistoryModel illnessModel);
    }
}

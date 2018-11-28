using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        Task<IActionResult> UpdateDoctorAvatar(string doctorId, IFormFile file);
        Task<string> GetDoctorAvatar(string doctorId);
        (bool status, string message) FillIllnessHistory(IllnessHistoryModel illnessModel);
    }
}

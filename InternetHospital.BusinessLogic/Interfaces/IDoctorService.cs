using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.BusinessLogic.Models.DoctorBlackList;
using Microsoft.AspNetCore.Http;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IDoctorService
    {
        DoctorDetailedModel Get(int id);

        (IEnumerable<DoctorModel> doctors, int count) GetFilteredDoctors(DoctorSearchParameters queryParameters);
        IEnumerable<SpecializationModel> GetAvailableSpecialization();
        PageModel<IEnumerable<PreviousAppointmentsModel>> GetPreviousAppointments(AppointmentHistoryParameters parameters, int doctorId);
        Task<bool> UpdateDoctorInfo(DoctorProfileModel doctorModel, int userId,
        IFormFileCollection passport, IFormFileCollection diploma, IFormFileCollection license);
        DoctorProfileModel GetDoctorProfile(int userId);
        Task<bool> UpdateDoctorAvatar(string doctorId, IFormFile file);
        Task<string> GetDoctorAvatar(string doctorId);
        (bool status, string message) FillIllnessHistory(IllnessHistoryModel illnessModel);
        FilteredModel<MyPatientModel> GetMyPatients(int doctorId, MyPatientsSearchParameters queryParameters);
        FilteredModel<MyBlackList> GetBlackList(int doctorId, MyPatientsSearchParameters queryParameters);
        IEnumerable<string> GetAppointmentStatuses();
        bool AddToBlackList(AddToBlackListModel creationModel, int doctorId);
        bool RemoveFromBlackList(int[] patientId, int doctorId);
    }
}

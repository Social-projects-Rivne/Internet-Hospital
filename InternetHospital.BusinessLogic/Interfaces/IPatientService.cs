using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IPatientService
    {
        Task<bool> UpdatePatientInfo(PatientModel patientModel, int userId, IFormFileCollection files);
        Task<PatientDetailedModel> Get(int id);
        (IEnumerable<IllnessHistoryModel> histories, int count) GetFilteredHistories(IllnessHistorySearchModel queryParameters);
        Task<PatientModel> GetPatientProfile(int userId);
    }
}

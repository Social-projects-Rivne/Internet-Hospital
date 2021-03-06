﻿using InternetHospital.BusinessLogic.Models;
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
        Task<bool> UpdateToDoctor(PatientToDoctorModel patientModel, int userId, IFormFileCollection diploma, IFormFileCollection license);
        Task<(IEnumerable<IllnessHistoryModel> histories, int count)> GetFilteredHistories(IllnessHistorySearchModel queryParameters, string id);
        Task<PatientModel> GetPatientProfile(int userId);
    }
}

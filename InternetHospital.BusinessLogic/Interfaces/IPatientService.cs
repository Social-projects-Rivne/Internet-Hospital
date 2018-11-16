using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IPatientService
    {
        Task<bool> UpdatePatienInfo(PatientModel patientModel, int userId, IFormFileCollection files);
    }
}

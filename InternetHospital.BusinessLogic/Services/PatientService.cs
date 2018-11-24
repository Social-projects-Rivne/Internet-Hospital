using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationContext _context;
        private readonly IFilesService _uploadingFiles;

        public PatientService(ApplicationContext context, IFilesService uploadingFiles)
        {
            _context = context;
            _uploadingFiles = uploadingFiles;
        }

        public async Task<PatientDetailedModel> Get(int id)
        {
            PatientDetailedModel returnedPatient = null;
            var searchedPatient = await _context.Users.Include(p=> p.IllnessHistories).FirstOrDefaultAsync(pa=> pa.Id == id);
            if (searchedPatient != null)
            {  
                returnedPatient = Mapper.Map<User, PatientDetailedModel>(searchedPatient);
            }
            return returnedPatient;
        }

        public async Task<bool> UpdatePatienInfo(PatientModel patientModel, int userId, IFormFileCollection files)
        {
            var result = true;
            var patient = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (patient == null)
            {
                result = false;
            }
            else
            {
                _context.Update(patient);

                Mapper.Map<PatientModel, User>(patientModel,patient,
                                    cfg => cfg.AfterMap((pat, user) => user.LastStatusChangeTime = DateTime.Now));

                 var userWithPassport = await _uploadingFiles.UploadPassport(files, patient);

                if (userWithPassport != null)
                {
                    await _context.SaveChangesAsync();
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}

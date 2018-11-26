using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationContext _context;
        private readonly IFilesService _uploadingFiles;
        private readonly UserManager<User> _userManager;
        const string PATIENT = "Patient";
        
        public PatientService(ApplicationContext context, IFilesService uploadingFiles, UserManager<User> userManager)
        {
            _context = context;
            _uploadingFiles = uploadingFiles;
            _userManager = userManager;
        }

        public async Task<PatientModel> GetPatientProfile(int userId)
        {
            var patient = await _userManager.FindByIdAsync(userId.ToString());
            var patientModel = Mapper.Map<User, PatientModel>(patient);
            return patientModel;
        }

        public async Task<bool> UpdatePatientInfo(PatientModel patientModel, int userId, IFormFileCollection files)
        {
            var addedTime = DateTime.Now;
            var patient = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (patient == null)
            {
                return false;
            }

            var temporaryPatient = Mapper.Map<PatientModel, TemporaryUser>(patientModel, 
                config => config.AfterMap((src, dest) => 
                {
                    dest.AddedTime = addedTime;
                    dest.Role = PATIENT;
                    dest.UserId = patient.Id;
                }));

            _context.Add(temporaryPatient);
            _context.Update(patient);

            await _uploadingFiles.UploadPassport(files, patient, addedTime);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

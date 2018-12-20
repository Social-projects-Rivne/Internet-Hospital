using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public async Task<(IEnumerable<IllnessHistoryModel> histories, int count)> GetFilteredHistories(IllnessHistorySearchModel queryParameters, string id)
        {
            var patient = await _userManager.FindByIdAsync(id);
            var histories = _context.IllnessHistories.Where(p => p.UserId == patient.Id).OrderBy(d => d.ConclusionTime).AsQueryable();
            if (queryParameters.SearchFromDate != null)
            {
                var fromDate = Convert.ToDateTime(queryParameters.SearchFromDate);
                histories = histories
                    .Where(d => d.ConclusionTime >= fromDate);
            }
            if (queryParameters.SearchToDate != null)
            {
                var toDate = Convert.ToDateTime(queryParameters.SearchToDate);
                toDate = toDate.AddDays(1);
                histories = histories.
                    Where(d => d.ConclusionTime <= toDate);
            }
            var historiesCount = histories.Count();
            var historiesResult = PaginationHelper(histories, queryParameters.PageCount, queryParameters.Page).ToList();
            return (historiesResult, historiesCount);
        }   
        private IQueryable<IllnessHistoryModel> PaginationHelper(IQueryable<IllnessHistory> histories, int pageCount, int page)
        {
            var historiesModel = histories
                .Skip(pageCount * (page - 1))
                .Take(pageCount).Select(x => new IllnessHistoryModel
                {
                    AppointmentId = x.AppointmentId ?? default,
                    Complaints = x.Complaints,
                    FinishAppointmentTimeStamp = ((DateTimeOffset)x.ConclusionTime).ToUnixTimeMilliseconds(),
                    Diagnose = x.Diagnose,
                    DiseaseAnamnesis = x.DiseaseAnamnesis,
                    LifeAnamnesis = x.LifeAnamnesis,
                    LocalStatus = x.LocalStatus,
                    ObjectiveStatus = x.ObjectiveStatus,
                    SurveyPlan = x.SurveyPlan,
                    TreatmentPlan = x.TreatmentPlan,
                    DoctorName = $"{x.Appointment.Doctor.User.FirstName} {x.Appointment.Doctor.User.SecondName} {x.Appointment.Doctor.User.ThirdName}"
                });

            return historiesModel;
        }

        public async Task<PatientModel> GetPatientProfile(int userId)
        {
            PatientModel patientModel = null;
            var patient = await _userManager.FindByIdAsync(userId.ToString());
            if (patient != null)
            {
                patientModel = Mapper.Map<User, PatientModel>(patient);
            }
            return patientModel;
        }

        public async Task<bool> UpdatePatientInfo(PatientModel patientModel, int userId, IFormFileCollection files)
        {
            const int EDIT_PROFILE_REQUEST_ID = 1;
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
                    dest.UserRequestTypeId = EDIT_PROFILE_REQUEST_ID;
                }));

            _context.Add(temporaryPatient);
            _context.Update(patient);

            await _uploadingFiles.UploadPassport(files, patient, addedTime);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateToDoctor(PatientToDoctorModel patientModel, int userId, IFormFileCollection diploma, IFormFileCollection license)
        {
            const int BECOME_A_DOCTOR_REQUEST_ID = 2;
            var addedTime = DateTime.Now;
            var patient = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (patient == null)
            {
                return false;
            }
            var model = Mapper.Map<User, PatientModel>(patient);
            var temporaryPatient = Mapper.Map<PatientModel, TemporaryUser>(model,
                config => config.AfterMap((src, dest) =>
                {
                    dest.AddedTime = addedTime;
                    dest.Role = PATIENT;
                    dest.UserId = patient.Id;
                    dest.Address = patientModel.Address;
                    dest.Specialization = patientModel.Specialization;
                    dest.isRejected = false;
                    dest.UserRequestTypeId = BECOME_A_DOCTOR_REQUEST_ID;
                }));

            _context.Add(temporaryPatient);
            _context.Update(patient);

            await _uploadingFiles.UploadDiploma(diploma, patient, addedTime);
            await _uploadingFiles.UploadLicense(license, patient, addedTime);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext _context;
        private const int MINIMUM_APPOINTMENT_TIME = 15;
        private const int DEFAULT_STATUS = 1;
        private const int RESERVED_STATUS = 2;

        public AppointmentService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all open and reserved appointments for current doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public IQueryable<AppointmentModel> GetMyAppointments(int doctorId)
        {
            var appointments = _context.Appointments
                .Where(a => (a.DoctorId == doctorId) 
                            && (a.StatusId == DEFAULT_STATUS || a.StatusId == RESERVED_STATUS))
                .Select(a => new AppointmentModel
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    UserName = a.User.FirstName + a.User.SecondName,
                    Address = a.Address,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Status = a.Status.Name
                });
            return appointments;
        }

        /// <summary>
        /// Create an appointment
        /// </summary>
        /// <param name="creationModel"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public (bool status, string message) AddAppointment(AppointmentCreationModel creationModel, int doctorId)
        {
            if (DateTime.Now > creationModel.StartTime)
            {
                return (false, "You don't have a time machine");
            }
            if (creationModel.StartTime > creationModel.EndTime)
            {
                return (false, "Appointment can't start after ending");
            }
            if ((creationModel.EndTime - creationModel.StartTime).TotalMinutes < MINIMUM_APPOINTMENT_TIME)
            {
                return (false, $"Your appointment has to be at least {MINIMUM_APPOINTMENT_TIME} minutes");
            }
            if (creationModel.StartTime.Date != creationModel.EndTime.Date)
            {
                return (false, "Your appointment can't be bigger then one day");
            }
            var isUnique = _context.Appointments.Any(a => a.DoctorId == doctorId
                                                       && ((creationModel.StartTime >= a.StartTime && creationModel.StartTime < a.EndTime)
                                                           || (creationModel.EndTime > a.StartTime && creationModel.EndTime <= a.EndTime)));
            if (isUnique)
            {
                return (false, "You already have an appointment for this time");
            }

            return CreateAppointment(creationModel, doctorId) ? (true, "Appointment has been successfully created") : (false, "Error adding an appointment");
        }

        /// <summary>
        /// Get all available appointments of selected doctor 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public IQueryable<AvailableAppointmentModel> GetAvailableAppointments(int doctorId)
        {
            var appointments = _context.Appointments
                .Where(a => (a.DoctorId == doctorId) && (a.StatusId == DEFAULT_STATUS))
                .Select(a => new AvailableAppointmentModel
                {
                    Id = a.Id,
                    Address = a.Address,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                });
            return appointments;
        }

        private bool CreateAppointment(AppointmentCreationModel model, int id)
        {
            try
            {
                var appointment = Mapper.Map<Appointment>(model);
                appointment.StatusId = DEFAULT_STATUS;
                appointment.DoctorId = id;
                if (model.Address == null)
                    appointment.Address = _context.Doctors.FirstOrDefault(d => d.UserId == id)?.Address;
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

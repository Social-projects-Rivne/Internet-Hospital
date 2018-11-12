﻿using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext _context;
        private const int MINIMUM_APPOINTMENT_TIME = 15;
        private const int MINIMUM_TIME_BEFORE_APPOINTMENT = 10;
        private const int TIME_FOR_FINISHING_APPOINTMENT = 12;

        public AppointmentService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all open and reserved appointments for current doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>
        /// returns a list of doctor's appointments
        /// </returns>
        public IEnumerable<AppointmentModel> GetMyAppointments(int doctorId)
        {
            var appointments = _context.Appointments
                .Where(a => (a.DoctorId == doctorId)
                            && (a.StatusId == (int) AppointmentStatuses.DEFAULT_STATUS 
                                || a.StatusId == (int) AppointmentStatuses.RESERVED_STATUS))
                .Select(a => new AppointmentModel
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    UserFirstName = a.User.FirstName,
                    UserSecondName = a.User.SecondName,
                    Address = a.Address,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Status = a.Status.Name
                });
            return appointments.ToList();
        }

        /// <summary>
        /// Create an appointment
        /// </summary>
        /// <param name="creationModel"></param>
        /// <param name="doctorId"></param>
        /// <returns>
        /// returns status and message of appointment creation
        /// </returns>
        public (bool status, string message) AddAppointment(AppointmentCreationModel creationModel, int doctorId)
        {
            DeleteUncommittedAppointments(doctorId);

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
            var isReserved = _context.Appointments.Any(a => a.DoctorId == doctorId
                                                       && ((creationModel.StartTime >= a.StartTime && creationModel.StartTime < a.EndTime)
                                                           || (creationModel.EndTime > a.StartTime && creationModel.EndTime <= a.EndTime)));
            if (isReserved)
            {
                return (false, "You already have an appointment for this time");
            }

            var ifUnfinished = _context.Appointments.Any(a => a.DoctorId == doctorId
                                                              && (DateTime.Now - a.EndTime).TotalHours > TIME_FOR_FINISHING_APPOINTMENT
                                                              && (a.StatusId == (int) AppointmentStatuses.RESERVED_STATUS));
            if (ifUnfinished)
            {
                return (false, "You have out of date appointments. Please close it before creating new");
            }

            return CreateAppointment(creationModel, doctorId) ? (true, "Appointment has been successfully created") : (false, "Error adding an appointment");
        }

        /// <summary>
        /// Get all available appointments of selected doctor 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>
        /// returns a list of appointments what can be reserved by patient
        /// </returns>
        public IEnumerable<AvailableAppointmentModel> GetAvailableAppointments(int doctorId)
        {
            var appointments = _context.Appointments
                .Where(a => (a.DoctorId == doctorId)
                            && (a.StatusId == (int) AppointmentStatuses.DEFAULT_STATUS)
                            && (a.StartTime - DateTime.Now).TotalMinutes > MINIMUM_TIME_BEFORE_APPOINTMENT)
                .Select(a => new AvailableAppointmentModel
                {
                    Id = a.Id,
                    Address = a.Address,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                });
            return appointments.ToList();
        }

        /// <summary>
        /// Delete doctor's appointment if it's not reserved
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="doctorId"></param>
        /// <returns>
        /// returns status and message about deleting an appointment
        /// </returns>
        public (bool status, string message) DeleteAppointment(int appointmentId, int doctorId)
        {
            var appointment = _context.Appointments
                .FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                return (false, "Appointment not found");
            }

            if (appointment.DoctorId != doctorId)
            {
                return (false, "You can delete only your appointments");
            }

            if (appointment.StatusId != (int) AppointmentStatuses.DEFAULT_STATUS)
            {
                return (false, "This appointment is reserved. You can only cancel it");
            }

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return (true, "Appointment was deleted");
        }

        /// <summary>
        /// cancel appointment if it was already reserved
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="doctorId"></param>
        /// <returns>
        /// returns status and message of appointment cancellation
        /// </returns>
        public (bool status, string message) CancelAppointment(int appointmentId, int doctorId)
        {
            var appointment = _context.Appointments
                .FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                return (false, "Appointment not found");
            }

            if (appointment.DoctorId != doctorId)
            {
                return (false, "You can cancel only your appointments");
            }

            if (appointment.StatusId != (int) AppointmentStatuses.RESERVED_STATUS)
            {
                return (false, "You can cancel only reserved appointments");
            }

            appointment.StatusId = (int) AppointmentStatuses.CANCELED_STATUS;
            _context.SaveChanges();

            return (true, "Appointment was canceled");
        }

        /// <summary>
        /// finish appointment if patient reserved it and came to doctor
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="doctorId"></param>
        /// <returns>
        /// returns status and message of appointment finishing
        /// </returns>
        public (bool status, string message) FinishAppointment(int appointmentId, int doctorId)
        {
            var appointment = _context.Appointments
                .FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                return (false, "Appointment not found");
            }

            if (appointment.DoctorId != doctorId)
            {
                return (false, "You can finish only your appointments");
            }

            if (appointment.StatusId != (int) AppointmentStatuses.RESERVED_STATUS)
            {
                return (false, "You can finish only reserved appointments");
            }

            appointment.StatusId = (int) AppointmentStatuses.FINISHED_STATUS;
            _context.SaveChanges();

            return (true, "Appointment was finished");
        }

        private bool CreateAppointment(AppointmentCreationModel model, int id)
        {
            try
            {
                var appointment = Mapper.Map<Appointment>(model);
                appointment.StatusId = (int) AppointmentStatuses.DEFAULT_STATUS;
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

        private bool DeleteUncommittedAppointments(int doctorId)
        {
            try
            {
                var uncommittedAppointments = _context.Appointments
                    .Where(a => a.DoctorId == doctorId
                                && a.StatusId == (int) AppointmentStatuses.DEFAULT_STATUS
                                && DateTime.Now > a.StartTime);
                _context.RemoveRange(uncommittedAppointments);
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
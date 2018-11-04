using AutoMapper;
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

        public AppointmentService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<AppointmentModel> GetDoctorAppointments(int doctorId)
        {
            var appointments = _context.Appointments
                .Where(x => x.DoctorId == doctorId)
                .Select(x => new AppointmentModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.FirstName + x.User.SecondName,
                    Address = x.Address,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status.Name
                });
            return appointments;
        }

        public (bool status, string message) AddAppointment(AppointmentCreationModel creationModel, int doctorId)
        {
            creationModel.StartTime = creationModel.StartTime.AddHours(2);
            creationModel.EndTime = creationModel.EndTime.AddHours(2);
            //validation

            if (DateTime.Now > creationModel.StartTime)
                return (false, "You don't have a time machine");
            if (creationModel.StartTime > creationModel.EndTime)
                return (false, "Appointment can't start after ending");
            if ((creationModel.EndTime - creationModel.StartTime).TotalMinutes < 10)
                return (false, "Your appointment has to be at least 10 minutes");
            if (creationModel.StartTime.Date != creationModel.EndTime.Date)
                return (false, "Your appointment can't be bigger then one day");

            var num = _context.Appointments.Count(x => x.DoctorId == doctorId
                                                       && ((creationModel.StartTime >= x.StartTime && creationModel.StartTime < x.EndTime)
                                                           || (creationModel.EndTime > x.StartTime && creationModel.EndTime <= x.EndTime)));
            if (num > 0)
                return (false, "You already have an appointment for this time");

            //service
            var appointment = Mapper.Map<Appointment>(creationModel);
            appointment.StatusId = 1;
            appointment.DoctorId = doctorId;
            if (creationModel.Address == null)
                appointment.Address = _context.Doctors.FirstOrDefault(x=>x.UserId==doctorId)?.Address;
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return (true, "Appointment has been successfully created");
        }
    }
}

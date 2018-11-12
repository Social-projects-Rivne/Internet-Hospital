﻿using System.Collections.Generic;
using InternetHospital.BusinessLogic.Models;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<AppointmentModel> GetMyAppointments(int doctorId);
        IEnumerable<AvailableAppointmentModel> GetAvailableAppointments(int doctorId);
        (bool status, string message) AddAppointment(AppointmentCreationModel creationModel, int doctorId);
        (bool status, string message) CancelAppointment(int appointmentId, int doctorId);
        (bool status, string message) DeleteAppointment(int appointmentId, int doctorId);
        (bool status, string message) FinishAppointment(int appointmentId, int doctorId);
    }
}
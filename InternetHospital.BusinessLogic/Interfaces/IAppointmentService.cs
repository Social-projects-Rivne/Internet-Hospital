using System.Collections.Generic;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<AppointmentModel> GetMyAppointments(int doctorId);
        PageModel<List<AppointmentModel>> GetMyAppointmentsHistory(AppointmentHistoryParameters parameters,int doctorId);
        PageModel<List<AvailableAppointmentModel>> GetAvailableAppointments(AppointmentSearchModel searchParameters);
        IEnumerable<AppointmentForPatient> GetPatientsAppointments(int patientId);
        bool AddAppointment(AppointmentCreationModel creationModel, int doctorId);
        bool SubscribeForAppointment(int appointmentId, int patientId);
        bool UnsubscribeForAppointment(int appointmentId, int patientId);
        (bool status, string message) CancelAppointment(int appointmentId, int doctorId);
        (bool status, string message) DeleteAppointment(int appointmentId, int doctorId);
    }
}

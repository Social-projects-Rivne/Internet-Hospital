using InternetHospital.BusinessLogic.Models;
using System.Linq;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IQueryable<AppointmentModel> GetMyAppointments(int doctorId);
        IQueryable<AvailableAppointmentModel> GetAvailableAppointments(int doctorId);
        (bool status, string message) AddAppointment(AppointmentCreationModel creationModel, int doctorId);
        (bool status, string message) CancelAppointment(int appointmentId, int doctorId);
        (bool status, string message) DeleteAppointment(int appointmentId, int doctorId);
        (bool status, string message) FinishAppointment(int appointmentId, int doctorId);
    }
}

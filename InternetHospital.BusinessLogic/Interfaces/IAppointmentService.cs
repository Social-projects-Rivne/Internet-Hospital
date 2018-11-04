using InternetHospital.BusinessLogic.Models;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<AppointmentModel> GetDoctorAppointments(int doctorId);
        (bool status, string message) AddAppointment(AppointmentCreationModel creationModel, int doctorId);
    }
}

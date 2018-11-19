using System;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class AppointmentCreationModel
    {
        public string Address { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

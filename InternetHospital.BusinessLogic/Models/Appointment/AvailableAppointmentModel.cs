using System;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class AvailableAppointmentModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

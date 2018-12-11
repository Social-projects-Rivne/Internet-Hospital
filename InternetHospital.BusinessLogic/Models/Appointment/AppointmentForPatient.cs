using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class AppointmentForPatient
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorSecondName { get; set; }
        public string DoctorSpecialication { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public bool isAllowPatientInfo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

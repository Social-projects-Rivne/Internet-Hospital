using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class PreviousAppointmentsModel
    {
        public string UserFullName { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IllnessHistoryModel IllnessHistory { get; set; }
    }
}

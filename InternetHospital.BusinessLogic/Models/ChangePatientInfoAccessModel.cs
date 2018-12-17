using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class ChangePatientInfoAccessModel
    {
        public int AppointmentId { get; set; }
        public bool IsAllowPatientInfo { get; set; }
    }
}

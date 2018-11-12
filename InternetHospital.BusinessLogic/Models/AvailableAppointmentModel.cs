using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class AvailableAppointmentModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.DoctorBlackList
{
    public class MyBlackList
    {
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientSecondName { get; set; }
        public string PatientThirdName { get; set; }
        public DateTime DateOfBanned { get; set; }
        public string Description { get; set; }
    }
}

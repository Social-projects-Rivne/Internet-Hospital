using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class MyPatientModel
    {
        public int Id { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientSecondName { get; set; }
        public string PatientThirdName { get; set; }
        public string PatientEmail { get; set; }
        public string Banned { get; set; }
    }
}

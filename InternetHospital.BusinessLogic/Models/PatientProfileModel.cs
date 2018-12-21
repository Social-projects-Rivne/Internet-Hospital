using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class PatientProfileModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}

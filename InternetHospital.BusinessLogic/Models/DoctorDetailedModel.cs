using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class DoctorDetailedModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AvatarURL { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string DoctorsInfo { get; set; }
        public string Address { get; set; }
        public string LicenseURL { get; set; }
        public string[] DiplomasURL { get; set; }

    }
}

using InternetHospital.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class DoctorsDto
    {
        public DoctorsDto() { }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string SpecializationName { get; set; }
        public User User { get; set; }
        public Specialization Specialization { get; set; }
    }
}

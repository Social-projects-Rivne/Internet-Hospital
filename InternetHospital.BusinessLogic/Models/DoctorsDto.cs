using InternetHospital.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class DoctorsDto
    {
        public DoctorsDto() { }

        public DoctorsDto(Doctor doctor)
        {
            Id = doctor.UserId;
            FirstName = doctor.User.FirstName;
            SecondName = doctor.User.SecondName;
            ThirdName = doctor.User.ThirdName;
            SpecializationName = doctor.Specialization.Name;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string SpecializationName { get; set; }

        public virtual User User { get; set; }

        public virtual Specialization Specialization { get; set; }
    }
}

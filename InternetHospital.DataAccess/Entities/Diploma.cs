using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class Diploma
    {
        public int Id { get; set; }
        public string DiplomaURL { get; set; }
        public bool IsValid { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}

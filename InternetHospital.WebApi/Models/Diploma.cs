using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.WebApi.Models
{
    public class Diploma
    {
        public int Id { get; set; }
        public string DiplomaURL { get; set; }
        public bool IsValid { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
    }
}

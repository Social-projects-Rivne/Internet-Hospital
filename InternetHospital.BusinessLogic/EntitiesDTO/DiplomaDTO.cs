using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class DiplomaDTO
    {
        public int Id { get; set; }
        public string DiplomaURL { get; set; }
        public bool IsValid { get; set; }

        public DoctorDTO DoctorDTO { get; set; }
        public int DoctorId { get; set; }
    }
}

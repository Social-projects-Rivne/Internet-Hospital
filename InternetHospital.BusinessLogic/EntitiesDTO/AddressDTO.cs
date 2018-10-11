using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public PatientDTO PatientDTO { get; set; }

        public DoctorDTO DoctorDTO { get; set; }
    }
}

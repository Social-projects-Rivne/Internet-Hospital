using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class DoctorDTO
    {
        public int PatientId { get; set; }
        public PatientDTO PatientDTO { get; set; }

        public string LicenseURL { get; set; }
        public string DoctorsInfo { get; set; }
        public bool IsApproved { get; set; }

        public SpecializationDTO SpecializationDTO { get; set; }
        public int? SpecializationId { get; set; }

        public AddressDTO AddressDTO { get; set; }
        public int? WorkingAddressId { get; set; }

        public ICollection<DiplomaDTO> DiplomasDTO { get; set; }
    }
}

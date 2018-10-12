using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetHospital.DataAccess.Entities
{
    public class Doctor
    {
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public string LicenseURL { get; set; }
        public string DoctorsInfo { get; set; }
        public bool IsApproved { get; set; }

        public virtual Specialization Specialization { get; set; }
        public int? SpecializationId { get; set; }

        public virtual Address Address { get; set; }
        public int? WorkingAddressId { get; set; }

        public virtual ICollection<Diploma> Diplomas { get; set; }
    }
}

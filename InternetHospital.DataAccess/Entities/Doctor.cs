using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetHospital.DataAccess.Entities
{
    public class Doctor
    {
        public int UserId { get; set; }
        public int? SpecializationId { get; set; }
        public bool? IsApproved { get; set; }
        public string DoctorsInfo { get; set; }
        public string Address { get; set; }
        public string LicenseURL { get; set; }

        public virtual User User { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Diploma> Diplomas { get; set; }
    }
}

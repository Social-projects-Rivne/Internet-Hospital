using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class Doctor
    {
        public int UserId { get; set; }
        public int? SpecializationId { get; set; }
        public bool? IsApproved { get; set; }
        public string DoctorsInfo { get; set; }
        public string Address { get; set; }

        public virtual User User { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<IllnessHistory> IllnessHistories { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}

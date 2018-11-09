using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class AppointmentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}

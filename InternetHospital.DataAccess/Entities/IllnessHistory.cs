using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class IllnessHistory
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int DoctorId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnose { get; set; }
        public string Treatment { get; set; }
        public DateTime ConclusionTime { get; set; }

        public virtual User User { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

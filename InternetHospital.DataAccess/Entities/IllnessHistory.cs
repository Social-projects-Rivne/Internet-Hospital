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
        public int? AppointmentId { get; set; }
        public string Complaints { get; set; }
        public string DiseaseAnamnesis { get; set; }
        public string LifeAnamnesis { get; set; }
        public string ObjectiveStatus { get; set; }
        public string LocalStatus { get; set; }
        public string Diagnose { get; set; }
        public string SurveyPlan { get; set; }
        public string TreatmentPlan { get; set; }
        public DateTime ConclusionTime { get; set; }

        public virtual User User { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}

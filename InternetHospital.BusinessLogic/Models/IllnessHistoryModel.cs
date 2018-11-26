using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class IllnessHistoryModel
    {
        public int AppointmentId { get; set; }
        public DateTime ConclusionTime { get; set; }
        public string Complaints { get; set; }
        public string DiseaseAnamnesis { get; set; }
        public string LifeAnamnesis { get; set; }
        public string ObjectiveStatus { get; set; }
        public string LocalStatus { get; set; }
        public string Diagnose { get; set; }
        public string SurveyPlan { get; set; }
        public string TreatmentPlan { get; set; }
    }
}

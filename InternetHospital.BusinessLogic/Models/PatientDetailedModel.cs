using InternetHospital.DataAccess.Entities;
using System;

namespace InternetHospital.BusinessLogic.Models
{
    public class PatientDetailedModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        //public string PhoneNumber { get; set; }
        public IllnessHistoryModel[] IllnessHistory { get; set; }
    }
}

using System;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public bool IsAllowPatientInfo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IllnessHistoryModel IllnessHistory { get; set; }
    }

    public enum AppointmentStatuses
    {
        DEFAULT_STATUS = 1,
        RESERVED_STATUS,
        CANCELED_STATUS,
        FINISHED_STATUS,
        MISSED_STATUS
    };
}

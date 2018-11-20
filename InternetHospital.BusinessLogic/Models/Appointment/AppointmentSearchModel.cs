using System;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class AppointmentSearchModel
    {
        public int DoctorId { get; set; }
        private const int MAX_PAGE_COUNT = 15;
        private const int DEFAULT_PAGE = 1;
        public int Page { get; set; } = DEFAULT_PAGE;
        private int _pageCount = MAX_PAGE_COUNT;
        public int PageCount
        {
            get => _pageCount;
            set => _pageCount = (value > MAX_PAGE_COUNT) ? MAX_PAGE_COUNT : value;
        }
        private DateTime _from = DateTime.Now;
        public DateTime? From
        {
            get => _from;
            set => _from = (value == null || value.Value < DateTime.Now) ? DateTime.Now : value.Value;
        }
        public DateTime? Till { get; set; } = null;
    }
}

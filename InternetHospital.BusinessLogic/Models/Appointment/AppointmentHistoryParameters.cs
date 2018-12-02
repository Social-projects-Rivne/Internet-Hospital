using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.Appointment
{
    public class AppointmentHistoryParameters
    {
        private const int DEFAULT_PAGE = 1;
        private const int MAX_PAGE_COUNT = 15;
        public int Page { get; set; } = DEFAULT_PAGE;
        private int _pageCount = MAX_PAGE_COUNT;
        public int PageCount
        {
            get => _pageCount;
            set => _pageCount = (value > MAX_PAGE_COUNT) ? MAX_PAGE_COUNT : value;
        }
        public DateTime? From { get; set; }
        public DateTime? Till { get; set; } = null;
        public List<int> Statuses { get; set; } = new List<int>();
        public string SearchByName { get; set; }
    }
}

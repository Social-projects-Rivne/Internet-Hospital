using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class DoctorSearchParameters
    {
        private const int MAX_PAGE_COUNT = 15;
        private const int DEFAULT_PAGE = 1;
        public int Page { get; set; } = DEFAULT_PAGE;
        private int _pageCount = MAX_PAGE_COUNT;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > MAX_PAGE_COUNT) ? MAX_PAGE_COUNT : value; }
        }
        public string SearchByName { get; set; }
        public int? SearchBySpecialization { get; set; }
    }
}

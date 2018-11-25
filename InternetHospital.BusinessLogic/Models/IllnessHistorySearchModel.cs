using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class IllnessHistorySearchModel
    {
        private const int MAX_PAGE_COUNT = 15;
        private const int DEFAULT_PAGE = 1;
        public int Page { get; set; } = DEFAULT_PAGE;
        private int _pageCount = MAX_PAGE_COUNT;
        public int PageCount
        {
            get => _pageCount;
            set => _pageCount = (value > MAX_PAGE_COUNT) ? MAX_PAGE_COUNT : value;
        }
        public string SearchFromDate { get; set; }
        public string SearchToDate { get; set; }
    }
}

﻿namespace InternetHospital.BusinessLogic.Models
{
    public class UserSearchParameters
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
        public string SearchByName { get; set; }
        public int? SearchByStatus { get; set; }
    }
}

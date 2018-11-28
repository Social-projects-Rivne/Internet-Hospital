using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class ArticlesFilteringModel
    {
        public string Search { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int[] TypeIds { get; set; }
        private const int DEFAULT_PAGE = 1;
        public bool IncludeAll { get; set; } = false;
        public int Page { get; set; } = DEFAULT_PAGE;
        public int PageSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class UserListSearchParameters
    {
        private const int DEFAULT_PAGE = 1;
        public int Page { get; set; } = DEFAULT_PAGE;
        public int PageSize { get; set; }
        public string SearchByName { get; set; }
        public bool IncludeAll { get; set; } = false;
        public string Sort { get; set; }
        public string Order { get; set; }
        public string SelectedStatus { get; set; }
    }
}

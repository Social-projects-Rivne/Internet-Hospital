using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Models
{
    public class PageConfig
    {
        private const int DEFAULT_PAGE = 1;
        public int PageSize { get; set; } = DEFAULT_PAGE;
        public int PageIndex { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class FilteredModel<T>
    {
        public ICollection<T> Results { get; set; }
        public int Amount { get; set; }
    }
}

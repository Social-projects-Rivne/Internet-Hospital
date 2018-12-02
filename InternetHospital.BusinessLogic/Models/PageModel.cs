using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class PageModel<T> where T:IEnumerable
    {
        public int EntityAmount { get; set; }
        public T Entities { get; set; }
    }
}

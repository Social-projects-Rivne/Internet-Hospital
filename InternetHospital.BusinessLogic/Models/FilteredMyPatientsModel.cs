using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class FilteredMyPatientsModel
    {
        public IEnumerable<MyPatientModel> MyPatients { get; set; }
        public int AmountOfAllFiltered { get; set; }
    }
}

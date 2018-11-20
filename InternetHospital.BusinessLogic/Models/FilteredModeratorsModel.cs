using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class FilteredModeratorsModel
    {
        public IEnumerable<ModeratorModel> Moderators { get; set; }
        public int AmountOfAllFiltered { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using InternetHospital.BusinessLogic.Models;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IModeratorService
    { 
        FilteredModeratorsModel GetFilteredModerators(ModeratorSearchParameters queryParameters);
    }
}

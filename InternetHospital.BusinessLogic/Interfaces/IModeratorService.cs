﻿using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IModeratorService
    {
        FilteredModeratorsModel GetFilteredModerators(ModeratorSearchParameters queryParameters);
        Task<(User, string)> CreateModeratorAsync(ModeratorCreatingModel moderatorCreatingModel);
        Task<(bool, string)> DeleteAsync(int id);
        Task<IEnumerable<(bool, string)>> DeleteAsync(int[] ids);
    }
}

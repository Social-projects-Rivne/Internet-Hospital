using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IModeratorService
    { 
        FilteredModeratorsModel GetFilteredModerators(ModeratorSearchParameters queryParameters);
        Task<(User, string)> CreateModeratorAsync(ModeratorCreatingModel vm);
        Task<(bool, string)> DeleteAsync(int id);
        Task<IEnumerable<(bool, string)>> DeleteAsync(int[] id);

    }
}

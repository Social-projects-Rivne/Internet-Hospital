using InternetHospital.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IRequestService
    {
        PageModel<IEnumerable<ChangeRoleModel>> GetRequestsPatientToDoctor(PageConfig pageConfig);
        //ChangeRoleModel GetDetailedUserRequest(int id);
        Task<(bool, string)> HandlePatientToDoctorRequest(int id, bool isApproved);
    }
}

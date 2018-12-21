using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.EditProfile;
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
        Task<(bool, string)> HandlePatientToDoctorRequest(int id, bool isApproved);
        PageModel<IEnumerable<UserEditProfile>> GetRequestsEditProfiel(PageConfig pageConfig);
        Task<(bool, string)> HandleEditUserProfile(int id, bool isApproved);
    }
}

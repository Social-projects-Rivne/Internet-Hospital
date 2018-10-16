using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IRegistrationService
    {
        Task<User> DoctorRegistration(UserRegistrationModel vm);
        Task<User> PatientRegistration(UserRegistrationModel vm);

    }
}

using InternetHospital.BusinessLogic.Models;
using System.Linq;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IDoctorService
    {
        (IQueryable<DoctorsDto> doctors, int count) GetAll(DoctorSearchParameters queryParameters);
    }
}

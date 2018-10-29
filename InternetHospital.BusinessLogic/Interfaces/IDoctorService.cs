using InternetHospital.BusinessLogic.Models;
using System.Linq;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IDoctorService
    {
        (IQueryable<DoctorModel> doctors, int count) GetAll(DoctorSearchParameters queryParameters);
    }
}

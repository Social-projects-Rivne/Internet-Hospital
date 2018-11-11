using System.Collections.Generic;
using InternetHospital.BusinessLogic.Models;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IDoctorService
    {
        DoctorDetailedModel Get(int id);

        (IEnumerable<DoctorModel> doctors, int count) GetFilteredDoctors(DoctorSearchParameters queryParameters);

        ICollection<SpecializationModel> GetAvailableSpecialization();
    }
}

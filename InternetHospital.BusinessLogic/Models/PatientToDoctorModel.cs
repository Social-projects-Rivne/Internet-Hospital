using System.ComponentModel.DataAnnotations;

namespace InternetHospital.BusinessLogic.Models
{
    public class PatientToDoctorModel
    {
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

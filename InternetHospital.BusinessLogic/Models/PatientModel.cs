using System.ComponentModel.DataAnnotations;

namespace InternetHospital.BusinessLogic.Models
{
    public class PatientModel
    {
        [Required]
        [MaxLength(60, ErrorMessage = "First Name is too long")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Second Name is too long")]
        public string SecondName { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Third Name is too long")]
        public string ThirdName { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}

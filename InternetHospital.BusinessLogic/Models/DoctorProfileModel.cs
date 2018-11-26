using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Models
{
    public class DoctorProfileModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string ThirdName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

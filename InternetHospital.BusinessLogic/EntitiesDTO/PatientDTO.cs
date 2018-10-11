using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class PatientDTO
    {
        public int UserId { get; set; }
        public UserDTO UserDTO { get; set; }

        public DoctorDTO DoctorDTO { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string AvatarURL { get; set; }
        public string PassportURL { get; set; }

        public AddressDTO AddressDTO { get; set; }
        public int? LivingAddressId { get; set; }
    }
}

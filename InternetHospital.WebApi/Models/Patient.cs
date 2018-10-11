using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.WebApi.Models
{
    public class Patient
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public Doctor Doctor { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string AvatarURL { get; set; }
        public string PassportURL { get; set; }

        public Address Address { get; set; }
        public int? LivingAddressId { get; set; }

    }
}

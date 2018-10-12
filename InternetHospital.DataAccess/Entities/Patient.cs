using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetHospital.DataAccess.Entities
{
    public class Patient
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string AvatarURL { get; set; }
        public string PassportURL { get; set; }

        public virtual Address Address { get; set; }
        public int? LivingAddressId { get; set; }
    }
}

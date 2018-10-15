using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class User:IdentityUser<int>
    {
        public virtual Doctor Doctor { get; set; }

        public virtual Status Status { get; set; }
        public int StatusId { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string AvatarURL { get; set; }
        public string PassportURL { get; set; }

        public virtual ICollection<RefreshToken> Tokens { get; set; }
    }
}

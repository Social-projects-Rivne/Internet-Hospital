using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string AvatarURL { get; set; }
        public string PassportURL { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime SignUpTime { get; set; }
        public DateTime LastStatusChangeTime { get; set; }
        public int StatusId { get; set; }
        public virtual ICollection<FeedBack> FeedBacks { set; get; }

        public virtual Doctor Doctor { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<RefreshToken> Tokens { get; set; }
    }
}

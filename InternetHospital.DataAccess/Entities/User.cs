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
        public DateTime? BirthDate { get; set; }
        public DateTime SignUpTime { get; set; }
        public DateTime LastStatusChangeTime { get; set; }
        public int StatusId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<RefreshToken> Tokens { get; set; }
        public virtual ICollection<IllnessHistory> IllnessHistories { get; set; }
        public virtual ICollection<FeedBack> FeedBacks { set; get; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<TemporaryUser> TemporaryUsers { get; set; }
        public virtual ICollection<Passport> Passports { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

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
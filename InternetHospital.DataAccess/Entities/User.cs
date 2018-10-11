﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        public virtual Status Status { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<RefreshToken> Tokens { get; set; }
        //public string PasswordHashed { get; set; }
        //public string PasswordSalt { get; set; }
    }
}
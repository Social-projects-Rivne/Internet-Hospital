using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Patient Patient { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        //public string PasswordHashed { get; set; }
        //public string PasswordSalt { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Models
{
    public class ChangeRoleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Specialization { get; set; }

        public DateTime RequestTime { get; set; }
        public List<string> Diplomas { get; set; }
        public string[] Passports { get; set; }
        public string License { get; set; }
        //public string Status { get; set; }
    }
}

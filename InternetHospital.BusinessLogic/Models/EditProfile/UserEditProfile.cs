using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.EditProfile
{
    public class UserEditProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string Address { get; set; }
        public List<string> Passport { get; set; }
        public string License { get; set; }
        public List<string> Diplomas { get; set; }
        public DateTime RequestTime { get; set; }
    }
}

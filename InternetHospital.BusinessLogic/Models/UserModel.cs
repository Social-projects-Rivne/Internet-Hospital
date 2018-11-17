using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class UserModel
    {
        public int Id { set; get; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string AvatarURL { get; set; }
        public DateTime? BirthDate { get; set; }
        public int StatusId { get; set; }
    }
}

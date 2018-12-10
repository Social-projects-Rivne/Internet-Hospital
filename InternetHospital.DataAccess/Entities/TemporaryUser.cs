using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.Entities
{
    public class TemporaryUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime AddedTime { get; set; }
        public User User { get; set; }
    }
}

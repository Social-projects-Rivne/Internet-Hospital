using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class DoctorBlackList
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBlock { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual User User { get; set; }
    }
}

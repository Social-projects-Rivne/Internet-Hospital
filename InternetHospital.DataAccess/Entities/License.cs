using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.Entities
{
    public class License
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LicenseURL { get; set; }
        public bool? IsValid { get; set; }
        public DateTime AddedTime { get; set; }

        public virtual User User { get; set; }
    }
}

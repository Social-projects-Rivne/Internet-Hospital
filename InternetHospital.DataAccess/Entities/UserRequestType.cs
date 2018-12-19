using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class UserRequestType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<TemporaryUser> TemporaryUsers { get; set; }
    }
}

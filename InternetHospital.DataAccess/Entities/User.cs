using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class User:IdentityUser<int>
    {
        public virtual Patient Patient { get; set; }

        public virtual Status Status { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<RefreshToken> Tokens { get; set; }
    }
}

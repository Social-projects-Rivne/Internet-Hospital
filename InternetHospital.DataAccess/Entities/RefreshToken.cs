using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public bool Revoked { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}

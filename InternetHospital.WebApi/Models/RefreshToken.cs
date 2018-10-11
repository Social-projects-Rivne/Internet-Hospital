using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.WebApi.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public bool Revoked { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}

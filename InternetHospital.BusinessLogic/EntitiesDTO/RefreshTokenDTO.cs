using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class RefreshTokenDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public bool Revoked { get; set; }

        public UserDTO UserDTO { get; set; }
        public int UserId { get; set; }
    }
}

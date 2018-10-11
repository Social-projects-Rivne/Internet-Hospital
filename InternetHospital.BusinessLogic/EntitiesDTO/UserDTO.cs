using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public PatientDTO PatientDTO { get; set; }

        public RoleDTO RoleDTO { get; set; }
        public int RoleId { get; set; }

        public StatusDTO StatusDTO { get; set; }
        public int StatusId { get; set; }

        public ICollection<RefreshTokenDTO> TokensDTO { get; set; }
        //public string PasswordHashed { get; set; }
        //public string PasswordSalt { get; set; }
    }
}

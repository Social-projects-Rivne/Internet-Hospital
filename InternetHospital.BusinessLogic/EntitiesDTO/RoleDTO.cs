using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.EntitiesDTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserDTO> UsersDTO { get; set; }
    }
}

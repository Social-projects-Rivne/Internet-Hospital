using System;

namespace InternetHospital.DataAccess.Entities
{
    public class Diploma
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DiplomaURL { get; set; }
        public bool? IsValid { get; set; }
        public DateTime AddedTime { get; set; }

        public virtual User User { get; set; }
    }
}

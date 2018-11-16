using System;

namespace InternetHospital.DataAccess.Entities
{
    public class Passport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Ulr1 { get; set; }
        public string Ulr2 { get; set; }
        public string Ulr3 { get; set; }
        public bool? IsValid { get; set; }
        public DateTime LastImagesChangeTime { get; set; }

        public virtual User User { get; set; }
     
        //public int Id { get; set; }
        //public int UserId { get; set; }
        //public string PassportURL { get; set; }
        //public bool? IsValid { get; set; }
        //public DateTime AddedTime { get; set; }

        //public virtual User User { get; set; }
    }
}

namespace InternetHospital.DataAccess.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}

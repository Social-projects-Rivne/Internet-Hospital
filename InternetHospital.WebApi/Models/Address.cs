using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.WebApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }
    }
}

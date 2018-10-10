using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(new Address[]
            {
                new Address { Id = 1, Country = "Ukraine", Region = "Rivne", City = "Rive", Street = "Pushkina/11"},
                new Address { Id = 2, Country = "Ukraine", Region = "Rivne", City = "Rive", Street = "Pushkina/22"},
                new Address { Id = 3, Country = "Ukraine", Region = "Rivne", City = "Rive", Street = "Pushkina/33"}
            });
        }
    }
}

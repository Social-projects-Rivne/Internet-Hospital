using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role[]
            {
                new Role { Id = 1, Name = "Patient", Description = "Patient in our system"},
                new Role { Id = 2, Name = "Doctor", Description = "Doctor in our system"},
                new Role { Id = 3, Name = "Moderator", Description = "Moderator in our system"},
                new Role { Id = 4, Name = "Admin", Description = "Admin in our system"},
            });
        }
    }
}

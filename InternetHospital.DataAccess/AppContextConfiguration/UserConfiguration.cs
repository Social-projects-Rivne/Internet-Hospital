using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();

            builder.HasData(new User[]
            {
                new User { Id = 1, Email = "user@gmail.com", Password = "1111", RoleId = 1, StatusId = 2},
                new User { Id = 2, Email = "patient@gmail.com", Password = "2222", RoleId = 2, StatusId = 2 },
                new User { Id = 3, Email = "doctor@gmail.com", Password = "3333", RoleId = 3, StatusId = 2 }
            });
        }
    }
}

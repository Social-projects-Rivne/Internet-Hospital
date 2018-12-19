using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class UserRequestTypeConfiguration : IEntityTypeConfiguration<UserRequestType>
    {
        public void Configure(EntityTypeBuilder<UserRequestType> builder)
        {
            var userRequestTypes = new List<UserRequestType> {
                new UserRequestType {
                    Id = 1, TypeName = "Request to edit profile"
                },
                new UserRequestType {
                    Id = 2, TypeName = "Request to become a doctor"
                }
            };

            builder.HasData(userRequestTypes.ToArray());
        }
    }
}

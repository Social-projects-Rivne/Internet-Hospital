using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            #region InititalData
            int id = 1;
            var initialStatuses = new List<Status>
            {
                new Status
                {
                    Id = id++,
                    Name = "Banned",
                    Description = "Banned user who has violated our rules."
                },
                new Status
                {
                    Id = id++,
                    Name = "New",
                    Description = "New user registered in our system."
                },
                new Status
                {
                    Id = id++,
                    Name = "Approved",
                    Description = "Approved user with checked data."
                },
                new Status
                {
                    Id = id++,
                    Name = "Not approved",
                    Description = "Not approved, because user`s data was invalid."
                },
                new Status
                {
                    Id = id++,
                    Name = "Deleted",
                    Description = "Deleted user by Admin or Moderator."
                }
            };
            #endregion

            builder.HasData(initialStatuses.ToArray());
        }
    }
}
using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(new Status[]
            {
                new Status { Id = 1, Name = "Banned", Description = "Banned user!"},
                new Status { Id = 2, Name = "Working", Description = "Working user!"}
            });
        }
    }
}

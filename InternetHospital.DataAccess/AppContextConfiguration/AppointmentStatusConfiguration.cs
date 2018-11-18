using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    class AppointmenStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
        {
            #region InititalData
            int id = 1;
            var initialAppointments = new List<AppointmentStatus>
            {
                new AppointmentStatus
                {
                    Id = id++,
                    Name = "Open",
                    Description = "Appointment is created. No one signed up"
                },
                new AppointmentStatus
                {
                    Id = id++,
                    Name = "Reserved",
                    Description = "Patient signed up for appointment"
                },
                new AppointmentStatus
                {
                    Id = id++,
                    Name = "Canceled",
                    Description = "Doctor canceled an appointment"
                },
                new AppointmentStatus
                {
                    Id = id++,
                    Name = "Finished",
                    Description = "Patient has been accepted"
                },
                new AppointmentStatus
                {
                    Id = id++,
                    Name = "Missed",
                    Description = "Appointment is missed by patient"
                }
            };
            #endregion

            builder.HasData(initialAppointments.ToArray());
        }
    }
}
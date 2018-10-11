using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.PatientId);
            builder.Property(d => d.PatientId).ValueGeneratedNever();

            builder.Property(d => d.IsApproved).HasDefaultValue(false);
        
            builder.HasOne(d => d.Address).WithOne(a => a.Doctor).HasForeignKey<Doctor>("WorkingAddressId");

            builder.HasData(new Doctor[]
            {
                new Doctor {
                    PatientId = 2,
                    IsApproved = true,
                    LicenseURL = $"/Licenses/License{3}",
                    DoctorsInfo = "SuperPuperDoctor",
                    SpecializationId = 1,
                    WorkingAddressId = 2
                }
            });
        }
    }
}

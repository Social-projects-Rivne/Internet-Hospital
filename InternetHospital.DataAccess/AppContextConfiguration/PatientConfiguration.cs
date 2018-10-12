using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.UserId).ValueGeneratedNever();

            builder.HasOne(p => p.Address).WithOne(a => a.Patient).HasForeignKey<Patient>("LivingAddressId");



            //builder.HasData(new Patient[]
            //{
            //    new Patient {
            //        UserId = 2,
            //        FirstName = "PatientFirstName",
            //        SecondName = "PatientSecondName",
            //        ThirdName = "PatientThirdName",
            //        BirthDay =new DateTime(1999,06,24),
            //        PassportURL =$"/Passports/PassportId{2}",
            //        AvatarURL =$"/Avatars/AvatarId{2}",
            //        Phone = "38(096)-512-23-65",
            //        LivingAddressId = 1
            //    },

            //    new Patient {
            //        UserId = 3,
            //        FirstName = "DoctorFirstName",
            //        SecondName = "DoctorSecondName",
            //        ThirdName = "DoctorThirdName",
            //        BirthDay =new DateTime(1989,11,20),
            //        PassportURL =$"/Passports/PassportId{3}",
            //        AvatarURL =$"/Avatars/AvatarId{3}",
            //        Phone = "38(096)-512-23-65",
            //        LivingAddressId = 2
            //    }
            //});
        }
    }
}

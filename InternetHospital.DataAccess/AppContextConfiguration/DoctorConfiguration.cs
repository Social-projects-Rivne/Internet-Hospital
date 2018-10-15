using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.UserId);
            builder.Property(d => d.UserId).ValueGeneratedNever();

            builder.Property(d => d.IsApproved).HasDefaultValue(false);
        }
    }
}

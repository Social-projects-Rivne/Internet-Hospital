using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class DiplomaConfiguration : IEntityTypeConfiguration<Diploma>
    {
        public void Configure(EntityTypeBuilder<Diploma> builder)
        {
            builder.Property(d => d.IsValid).HasDefaultValue(false);
        }
    }
}

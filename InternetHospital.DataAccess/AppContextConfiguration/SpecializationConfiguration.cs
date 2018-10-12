using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasData(new Specialization[]
            {
                new Specialization { Id = 1, Name = "Terapevt", Description = "Default doctors specialization"}
            });
        }
    }
}

using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            const string BANNED = "Banned";
            const string NEW = "New";
            const string APPROVED = "Approved";
            const string NOT_APPROVED = "Not approved";

            builder.HasData(new Status[]
            {
                new Status { Id = 1, Name = BANNED, Description = "Banned user!"},
                new Status { Id = 2, Name = NEW, Description = "New user!"},
                new Status { Id = 3, Name = APPROVED, Description = "Approved user!"},
                new Status { Id = 4, Name = NOT_APPROVED, Description = "Not approved user!"}
            });
        }
    }
}

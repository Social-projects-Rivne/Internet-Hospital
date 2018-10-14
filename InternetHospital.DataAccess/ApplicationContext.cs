using InternetHospital.DataAccess.AppContextConfiguration;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetHospital.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor>  Doctors { get; set; }
        public DbSet<Address>  Addresses { get; set; }
        public DbSet<Diploma>  Diplomas { get; set; }
        public DbSet<RefreshToken>  Tokens { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new DiplomaConfiguration());
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new PatientConfiguration());
            builder.ApplyConfiguration(new SpecializationConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
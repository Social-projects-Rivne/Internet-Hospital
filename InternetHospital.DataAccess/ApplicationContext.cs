using InternetHospital.DataAccess.AppContextConfiguration;
using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<RefreshToken> Tokens{ get; set; }

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string DB = "HospitalDb";
            string Username = "postgres";
            string Password = "1111";
            optionsBuilder.UseLazyLoadingProxies()
                .UseNpgsql($"Host=localhost;Port=5432;Database={DB};Username={Username};Password={Password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<User>().Property<DateTime>("LastUpdated").HasComputedColumnSql("GetUtcDate()");
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DiplomaConfiguration());
        }
    }
}

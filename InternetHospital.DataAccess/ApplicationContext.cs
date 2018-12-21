using InternetHospital.DataAccess.AppContextConfiguration;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetHospital.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<RefreshToken> Tokens { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<FeedbackType> FeedbackTypes { set; get; }
        public DbSet<IllnessHistory> IllnessHistories { set; get; }
        public DbSet<Passport> Passports { set; get; }
        public DbSet<TemporaryUser> TemporaryUsers { set; get; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleAttachment> ArticleAttachments { get; set; }
        public DbSet<ArticleTypeArticle> ArticleTypeArticles { get; set; }
        public DbSet<ArticleEdition> ArticleEditions { get; set; }
        public DbSet<ArticleStatus> ArticleStatuses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<DoctorBlackList> DoctorBlackLists { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new AppointmenStatusConfiguration());
            builder.ApplyConfiguration(new SpecializationConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
            builder.ApplyConfiguration(new ArticleTypeArticleConfiguration());
            builder.ApplyConfiguration(new ArticleStatusConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
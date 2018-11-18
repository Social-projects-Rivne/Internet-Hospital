using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.WebApi.CustomMiddleware;

namespace InternetHospital.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //enable CORS
            services.AddCors();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            //configure entity framework
            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<ApplicationContext>(opt =>
                    {
                        opt.UseSqlServer(Configuration.GetConnectionString("AzureConnection"),
                                      m => m.MigrationsAssembly("InternetHospital.WebApi"));
                    });

            //configure identity
            services.AddIdentity<User, IdentityRole<int>>(config =>
                                                         {
                                                             config.User.RequireUniqueEmail = true;
                                                             config.SignIn.RequireConfirmedEmail = true;
                                                             config.Password.RequiredLength = 8;
                                                             config.Password.RequireNonAlphanumeric = false;
                                                         })
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();

            // configure jwt authentication            
            services.AddAuthentication(options =>
                                      {
                                          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                          options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                      })
                    .AddJwtBearer(options =>
                                 {
                                     options.TokenValidationParameters = new TokenValidationParameters
                                     {
                                         ValidateIssuer = true,
                                         ValidateAudience = false,
                                         ValidateLifetime = true,
                                         ValidateIssuerSigningKey = true,
                                         ValidIssuer = appSettings.JwtIssuer,
                                         ClockSkew = TimeSpan.Zero,
                                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                                                                                                                 .GetBytes(appSettings.JwtKey))
                                     };
                                 });

            //Add Authorization policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApprovedPatients",
                                  policyBuilder => policyBuilder.RequireAssertion(
                                                   context => context.User.HasClaim(claim =>
                                                                                    claim.Type == "ApprovedPatient")
                                                           && context.User.IsInRole("Patient")));

                options.AddPolicy("ApprovedDoctors",
                                  policyBuilder => policyBuilder.RequireAssertion(
                                                   context => context.User.HasClaim(claim =>
                                                              claim.Type == "ApprovedDoctor")
                                                           && context.User.IsInRole("Doctor")));
            });

            //Dependency injection
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IPatientService, PatientService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/Log_{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseCors(options => options.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials());

            app.UseAuthentication();

            Mapper.Initialize(config =>
            {
                config.CreateMap<UserRegistrationModel, User>();
                config.CreateMap<AppointmentCreationModel, Appointment>();
                config.CreateMap<FeedBackCreationModel, FeedBack>();
                config.CreateMap<PatientModel, User>();
                config.CreateMap<ModeratorCreatingModel, User>();
            });

            app.UseMvc();
        }
    }
}

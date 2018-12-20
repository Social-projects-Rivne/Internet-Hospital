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
using FluentValidation.AspNetCore;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.BusinessLogic.Models.Articles;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.BusinessLogic.Validation.AppointmentValidators;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using InternetHospital.WebApi.CustomMiddleware;
using InternetHospital.WebApi.Swagger;
using InternetHospital.BusinessLogic.Validation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using InternetHospital.BusinessLogic.Hubs;

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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(f =>
                    {
                        f.RegisterValidatorsFromAssemblyContaining<AppointmentCreationValidator>();
                        f.RegisterValidatorsFromAssemblyContaining<AppointmentSearchValidator>();
                        f.RegisterValidatorsFromAssemblyContaining<AppointmentSubscriptionValidator>();
                        f.RegisterValidatorsFromAssemblyContaining<IllnessHistoryCreationValidator>();
                        f.RegisterValidatorsFromAssemblyContaining<AppointmentHistoryValidator>();
                        f.RegisterValidatorsFromAssemblyContaining<AppointmentUnsubscribeValidator>();
                    });

            //allow to get user Id in BLL
            services.AddHttpContextAccessor();

            //enable CORS
            services.AddCors();

            //enable SignalR
            services.AddSignalR();

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
                                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtKey))
                                     };
                                     options.Events = new JwtBearerEvents
                                     {
                                         OnMessageReceived = context =>
                                         {
                                             var accessToken = context.Request.Query["access_token"];
                                             var path = context.HttpContext.Request.Path;
                                             if (!string.IsNullOrEmpty(accessToken) &&
                                                 (path.StartsWithSegments("/notifications")))
                                             {
                                                 context.Token = accessToken;
                                             }
                                             return Task.CompletedTask;
                                         }
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

            services.AddSwaggerServices();

            //Dependency injection
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IUserListService, UserListService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IModeratorService, ModeratorService>();
            services.AddScoped<IArticleTypeService, ArticleTypeService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddScoped<IRequestService, RequestService>();
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

            app.ConfigureSwagger();
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
                config.CreateMap<User, PatientDetailedModel>();
                config.CreateMap<PatientModel, TemporaryUser>();
                config.CreateMap<User, DoctorProfileModel>();
                config.CreateMap<DoctorProfileModel, TemporaryUser>();
                config.CreateMap<TemporaryUser, ChangeRoleModel>();
                config.CreateMap<UserModel, User>();
                config.CreateMap<Appointment, AvailableAppointmentModel>();
                config.CreateMap<ModeratorCreatingModel, User>();
                config.CreateMap<ArticleType, ArticleTypeModel>();
                config.CreateMap<ArticleCreatingModel, Article>();
                config.CreateMap<Article, SendingArticleEditingModel>();
                config.CreateMap<IllnessHistoryModel, IllnessHistory>();
                config.CreateMap<IllnessHistory, IllnessHistoryModel>();
                config.CreateMap<Appointment, PreviousAppointmentsModel>()
                .ForMember(prevAppoint => prevAppoint.UserFullName, opt => opt.MapFrom(o => $"{o.User.FirstName} {o.User.SecondName}"))
                .ForMember(prevAppoint => prevAppoint.Status, opt => opt.MapFrom(o => o.Status.Name));
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notifications");
            });

            app.UseMvc();
        }
    }
}

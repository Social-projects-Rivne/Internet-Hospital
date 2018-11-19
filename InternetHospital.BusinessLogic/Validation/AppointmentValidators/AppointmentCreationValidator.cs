using FluentValidation;
using System;
using System.Linq;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.DataAccess;
using Microsoft.AspNetCore.Http;

namespace InternetHospital.BusinessLogic.Validation.AppointmentValidators
{
    public class AppointmentCreationValidator : AbstractValidator<AppointmentCreationModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationContext _context;
        private const int MinimumAppointmentTime = 15;
        private const int TimeForFinishingAppointment = 12;

        public AppointmentCreationValidator(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(a => a.StartTime)
                .GreaterThan(DateTime.Now)
                .OverridePropertyName("message")
                .WithMessage("You don't have a time machine")
                .LessThanOrEqualTo(x => x.EndTime)
                .OverridePropertyName("message")
                .WithMessage("Appointment can't start after ending")
                .DependentRules(() =>
                {
                   RuleFor(a => a.StartTime.Date)
                        .Equal(a => a.EndTime.Date)
                        .OverridePropertyName("message")
                        .WithMessage("Your appointment can't be bigger then one day")
                        .DependentRules(() =>
                        {
                            RuleFor(a => (a.EndTime - a.StartTime).TotalMinutes)
                                .GreaterThanOrEqualTo(MinimumAppointmentTime)
                                .OverridePropertyName("message")
                                .WithMessage($"Your appointment has to be at least {MinimumAppointmentTime} minutes")
                                .DependentRules(() =>
                                {
                                    RuleFor(a => a)
                                        .Must(x => IsReserved(x.StartTime, x.EndTime))
                                        .OverridePropertyName("message")
                                        .WithMessage("You already have an appointment for this time")
                                        .DependentRules(() =>
                                        {
                                            RuleFor(a => a)
                                                .Must(a => IfUnfinished())
                                                .OverridePropertyName("message")
                                                .WithMessage("You have out of date appointments. Please close it before creating new");
                                        });
                                });
                        });
                });
        }

        private bool IsReserved(DateTime starTime, DateTime endTime)
        {
            var doctorId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            return !_context.Appointments.Any(a => a.DoctorId == doctorId
                                                  && ((starTime >= a.StartTime && starTime < a.EndTime)
                                                      || (endTime > a.StartTime && endTime <= a.EndTime)));
        }

        private bool IfUnfinished()
        {
            var doctorId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            return !_context.Appointments.Any(a => a.DoctorId == doctorId
                                                   && (DateTime.Now - a.EndTime).TotalHours > TimeForFinishingAppointment
                                                   && (a.StatusId == (int)AppointmentStatuses.RESERVED_STATUS));
        }
    }
}


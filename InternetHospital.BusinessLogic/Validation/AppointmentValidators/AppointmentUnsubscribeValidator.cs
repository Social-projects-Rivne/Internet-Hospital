using FluentValidation;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace InternetHospital.BusinessLogic.Validation.AppointmentValidators
{
    public class AppointmentUnsubscribeValidator : AbstractValidator<AppointmentUnsubscribeModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationContext _context;
        private Appointment _appointment;

        public AppointmentUnsubscribeValidator(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(a => a)
                .Must(a => IfExist(a.Id))
                .OverridePropertyName("message")
                .WithMessage("Appointment is not exist")
                .DependentRules(() =>
                {
                    RuleFor(a => a)
                         .Must(a => NotOpened())
                         .OverridePropertyName("message")
                         .WithMessage("This appointment is already opened")
                         .DependentRules(() =>
                         {
                             RuleFor(a => a)
                                 .Must(a => IsOver())
                                 .OverridePropertyName("message")
                                 .WithMessage("Appointment is over")
                                 .DependentRules(() =>
                                 {
                                     RuleFor(a => a)
                                         .Must(a => TooLate())
                                         .OverridePropertyName("message")
                                         .WithMessage("You can not unsubscribe until the appointment is less than 24 hours");
                                 });
                                                            
                         });
                });
        }
        
        private bool IfExist(int appointmentId)
        {
            _appointment = _context.Appointments
                .Include(a => a.Status)
                .FirstOrDefault(x => x.Id == appointmentId);

            return _appointment != null;
        }

        private bool NotOpened()
        {
            return _appointment.Status.Id == (int)AppointmentStatuses.RESERVED_STATUS;
        }

        private bool IsOver()
        {
            var timeNow = DateTime.Now;
            return timeNow < _appointment.StartTime;
        }

        private bool TooLate()
        {
            var timeNow = DateTime.Now;
            return timeNow < _appointment.StartTime.AddHours(-24);
        }
    }
}

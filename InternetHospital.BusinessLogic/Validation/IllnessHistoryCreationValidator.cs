using FluentValidation;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.DataAccess;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetHospital.BusinessLogic.Validation
{
    public class IllnessHistoryCreationValidator : AbstractValidator<IllnessHistoryModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationContext _context;

        public IllnessHistoryCreationValidator(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i)
                .Must(i => i.FinishAppointmentTime < DateTime.Now)
                .OverridePropertyName("message")
                .WithMessage("Please wait for the appointment beginning")
                .DependentRules(() =>
                {
                    RuleFor(i => i)
                    .Must(i => IsExits(i.AppointmentId))
                    .OverridePropertyName("message")
                    .WithMessage("Apoointment isn`t exist")
                    .DependentRules(() =>
                    {
                        RuleFor(i => i)
                        .Must(i => IsPatientAsigned(i.AppointmentId))
                        .OverridePropertyName("message")
                        .WithMessage("No one assigned to this appointment");
                    });
                });
        }

        private bool IsExits(int appointmentId)
        {
            var result = _context.Appointments.Any(a => a.Id == appointmentId);
            return result;
        }

        private bool IsPatientAsigned(int appointmentId)
        {
            var result = _context.Appointments
                .Where(a => a.Id == appointmentId)
                .Single()
                .StatusId == (int)AppointmentStatuses.RESERVED_STATUS;
            return result;
        }
    }
}
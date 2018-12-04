﻿using FluentValidation;
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
                .Must(i => Finish(i.FinishAppointmentTime))
                .OverridePropertyName("message")
                .WithMessage("Please wait for the appointment beginning")
                .DependentRules(() =>
                {
                    RuleFor(i => i)
                    .Must(i => IsExits(i.AppointmentId))
                    .OverridePropertyName("message")
                    .WithMessage("Apoointment with current doctor isn`t exist")
                    .DependentRules(() =>
                    {
                        RuleFor(i => i)
                        .Must(i => IsPatientAsigned(i.AppointmentId))
                        .OverridePropertyName("message")
                        .WithMessage("No one assigned to this appointment");
                    });
                });
        }

        private bool Finish(DateTime finishedTime)
        {
            const int maxMinuteDifference = 1;
            var utcDateTime = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Utc);
            var difference = utcDateTime - finishedTime;
            return difference.Minutes <= maxMinuteDifference;
        }

        private bool IsExits(int appointmentId)
        {
            var doctorId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            var result = _context.Appointments.Any(a => a.Id == appointmentId && a.DoctorId == doctorId);
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
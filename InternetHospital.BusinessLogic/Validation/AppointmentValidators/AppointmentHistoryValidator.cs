using System;
using FluentValidation;
using InternetHospital.BusinessLogic.Models.Appointment;

namespace InternetHospital.BusinessLogic.Validation.AppointmentValidators
{
    public class AppointmentHistoryValidator : AbstractValidator<AppointmentHistoryParameters>
    {
        public AppointmentHistoryValidator()
        {
            RuleFor(x => x)
                .Must(x => IsCorrectDate(x.From, x.Till))
                .OverridePropertyName("message")
                .WithMessage("Wrong date period");
        }

        private static bool IsCorrectDate(DateTime? startTime, DateTime? endTime)
        {
            if (startTime != null && endTime != null)
            {
                return endTime.Value > startTime.Value;
            }

            return true;
        }
    }
}

using System;
using FluentValidation;
using InternetHospital.BusinessLogic.Models.Appointment;

namespace InternetHospital.BusinessLogic.Validation.AppointmentValidators
{
    public class AppointmentSearchValidator : AbstractValidator<AppointmentSearchModel>
    {
        public AppointmentSearchValidator()
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

            if (endTime != null)
            {
                return endTime.Value > DateTime.Now;
            }

            return true;
        }
    }
}

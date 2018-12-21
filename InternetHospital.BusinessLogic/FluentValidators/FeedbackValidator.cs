using FluentValidation;
using InternetHospital.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.FluentValidators
{
    public class FeedbackValidator : AbstractValidator<FeedbackCreationModel>
    {
        public FeedbackValidator()
        {
            RuleFor(f => f.Text).NotEmpty().NotNull().MinimumLength(10);
            RuleFor(f => f.TypeId).NotEmpty();
        }
    }
}

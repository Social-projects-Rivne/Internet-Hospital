using FluentValidation;
using InternetHospital.BusinessLogic.Models.DoctorBlackList;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetHospital.BusinessLogic.Validation
{
    public class DoctorBlackListValidator : AbstractValidator<AddToBlackListModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationContext _context;
        private DoctorBlackList _blackList;

        public DoctorBlackListValidator(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p)
                .Must(p => AlreadyInBlackList(p.id))
                .OverridePropertyName("message")
                .WithMessage("The patient is in the black list");
        }

        public bool AlreadyInBlackList(int[] patientsId)
        {
            var doctorId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            bool checkPatients = true;
            foreach (var item in patientsId)
            {
                checkPatients = !_context.DoctorBlackLists.Any(b => b.DoctorId == doctorId
                                                  && b.UserId == item);
                if (!checkPatients)
                {
                    break;
                }
            }
            return checkPatients;

        }
    }
}

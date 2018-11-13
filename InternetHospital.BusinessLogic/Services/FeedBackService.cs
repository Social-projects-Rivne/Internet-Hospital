using AutoMapper;
using FluentValidation;
using InternetHospital.BusinessLogic.FluentValidators;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class FeedBackService : IFeedBackService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<FeedBackService> _logger;

        public FeedBackService(ApplicationContext context,
                               ILogger<FeedBackService> logger)
        {
            _logger = logger;
            _context = context;
        }

        public FeedBack FeedBackCreate(FeedBackCreationModel model)
        {
            FeedbackValidator validations = new FeedbackValidator();
            try
            {
                validations.ValidateAndThrow(model);

                var feedback = Mapper.Map<FeedBackCreationModel, FeedBack>
                    (model, cfg => cfg.AfterMap((src, dest) => dest.DateTime = DateTime.Now));
                _context.FeedBacks.Add(feedback);
                _context.SaveChanges();
                return feedback;
            }
            catch (Exception exception)
            {
               _logger.LogCritical($"{nameof(FeedBackService)} failed with {exception.Message}", exception);
                return null;
            }
        }

        public List<FeedBackType> GetFeedBackTypes()
        {
            return _context.FeedBackTypes.ToList();
        }
    }
}

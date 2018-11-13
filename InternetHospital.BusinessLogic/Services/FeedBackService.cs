using AutoMapper;
using Castle.Core.Logging;
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
        private readonly Castle.Core.Logging.ILogger _logger;

        public FeedBackService(ApplicationContext context,
                                Castle.Core.Logging.ILogger logger)
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
            catch (Exception ex)
            {
                _logger.Fatal($"{nameof(FeedBackService)} failed with {ex.Message}", ex);
                return null;
            }
        }

        public List<FeedBackType> GetFeedBackTypes()
        {
            return _context.FeedBackTypes.ToList();
        }
    }
}

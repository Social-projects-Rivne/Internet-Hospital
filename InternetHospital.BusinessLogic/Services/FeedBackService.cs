using AutoMapper;
using FluentValidation;
using InternetHospital.BusinessLogic.FluentValidators;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class FeedBackService : IFeedBackService
    {
        private readonly ApplicationContext _context;

        public FeedBackService(ApplicationContext context)
        {
            _context = context;
        }

        public FeedBack FeedBackCreate(FeedBackCreationModel model)
        {
            FeedbackValidator validations = new FeedbackValidator();
            try
            {
                validations.ValidateAndThrow(model);
                var feedback = Mapper.Map<FeedBackCreationModel, FeedBack>(model);
                feedback.DateTime = DateTime.Now;
                _context.FeedBacks.Add(feedback);
                _context.SaveChanges();
                return feedback;
            }
            catch
            {
                return null;
            }
        }
            
        public List<FeedBackType> GetFeedBackTypes()
        {
            return _context.FeedBackTypes.ToList();
        }
    }
}

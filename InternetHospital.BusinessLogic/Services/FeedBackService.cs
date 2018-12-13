using AutoMapper;
using FluentValidation;
using InternetHospital.BusinessLogic.FluentValidators;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
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

        public IEnumerable<FeedBackType> GetFeedBackTypes()
        {
            return _context.FeedBackTypes.ToList();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            List<FeedBack> localFeedbacks = _context.FeedBacks.ToList();
            List<UserModel> involvedUsers = new List<UserModel>();

            foreach (var item in localFeedbacks)
            {
                involvedUsers.AddRange(_context.Users
                    .Where(x => x.Id == item.UserId)
                    .Select(x => Mapper.Map<User, UserModel>(x))
                    );
            }
            if (involvedUsers != null)
                return involvedUsers;
            else
                return null;
        }

        public (IEnumerable<FeedbackViewModel>, int count) GetFeedbackViewModels(FeedbackSearchParams queryParameters)
        {
            List<FeedbackViewModel> feedbackViews = new List<FeedbackViewModel>();
            List<FeedBack> feedBacks = _context.FeedBacks.ToList();
            List<UserModel> users = GetUsers().ToList();
            List<FeedBackType> types = GetFeedBackTypes().ToList();

            for (int i = 0; i < feedBacks.Count; i++)
            {
                feedbackViews.Add(new FeedbackViewModel {
                    Id = feedBacks[i].Id,
                    FeedbackTypeId = feedBacks[i].TypeId,
                    Text = feedBacks[i].Text,
                    dateTime = feedBacks[i].DateTime,
                    IsViewed = feedBacks[i].IsViewed,
                    Reply = feedBacks[i].Reply,

                    UserId = users[i].Id,
                    UsersAvatarURL = users[i].AvatarURL,
                    UsersBirthDate = users[i].BirthDate,
                    UsersEmail = users[i].Email,
                    UsersFirstName = users[i].FirstName,
                    UsersSecondName = users[i].SecondName,
                    UsersThirdName = users[i].ThirdName,
                });
                feedBacks[i].IsViewed = false;

                foreach (var item in types)
                {
                    if (item.Id == feedBacks[i].TypeId)
                    {
                        feedbackViews[i].FeedbackTypeName = item.Name;
                    }
                }
            }
            
            if (queryParameters.SearchByName != null)
            {
                var toLowerSearchParameter = queryParameters.SearchByName.ToLower();
                feedbackViews = feedbackViews.Where(x =>
                x.Text.ToLower()
                .Contains(toLowerSearchParameter))
                .ToList();
            }
            if (queryParameters.SearchByType != null)
            {
                feedbackViews = feedbackViews.Where(x =>
                x.FeedbackTypeId == queryParameters.SearchByType).ToList();
            }

            var queryFeedbacks = feedbackViews
                .OrderByDescending(x => x.dateTime)
                .AsQueryable();
            var feedbacks = PaginationHelper<FeedbackViewModel>
                .GetPageValues(queryFeedbacks, queryParameters.PageCount, queryParameters.Page);

            return (feedbacks, feedbackViews.Count);
        }

        public FeedBack UpdateFeedBack(int id, FeedbackViewModel feedback)
        {
            FeedBack FeedbackEntity =  _context.FeedBacks.FirstOrDefault(x => x.Id == id);

            FeedbackEntity.IsViewed = feedback.IsViewed;
            FeedbackEntity.Reply = feedback.Reply;
            FeedbackEntity.Text = feedback.Text;

            _context.SaveChanges();

            return FeedbackEntity;
        }

    }
}

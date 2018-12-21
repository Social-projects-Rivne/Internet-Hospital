using AutoMapper;
using FluentValidation;
using InternetHospital.BusinessLogic.FluentValidators;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetHospital.BusinessLogic.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<FeedbackService> _logger;

        public FeedbackService(ApplicationContext context,
                               ILogger<FeedbackService> logger)
        {
            _logger = logger;
            _context = context;
        }

        public Feedback FeedbackCreate(FeedbackCreationModel model)
        {
            FeedbackValidator validations = new FeedbackValidator();
            try
            {
                validations.ValidateAndThrow(model);

                var feedback = Mapper.Map<FeedbackCreationModel, Feedback>
                    (model, cfg => cfg.AfterMap((src, dest) => dest.DateTime = DateTime.Now));
                _context.Feedbacks.Add(feedback);
                _context.SaveChanges();
                return feedback;
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"{nameof(FeedbackService)} failed with {exception.Message}", exception);
                return null;
            }
        }

        public IEnumerable<FeedbackType> GetFeedbackTypes()
        {
            return _context.FeedbackTypes.ToList();
        }

        public IEnumerable<UserModel> GetUsers(IEnumerable<Feedback> feedBacks)
        {
            ICollection<UserModel> involvedUsers = new List<UserModel>();

            var users = _context.Users;
            foreach (var item in feedBacks)
            {
                var user = users.Find(item.UserId);
                involvedUsers.Add(Mapper.Map<User, UserModel>(user));
            }

            return involvedUsers;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var feedbacks = _context.Feedbacks;
            ICollection<UserModel> involvedUsers = new List<UserModel>();

            var users = _context.Users;
            foreach (var item in feedbacks)
            {
                var user = users.Find(item.UserId);
                involvedUsers.Add(Mapper.Map<User, UserModel>(user));
            }

            return involvedUsers;
        }

        public PageModel<List<FeedbackViewModel>> GetFeedbackViewModels(FeedbackSearchParams queryParameters)
        {
            List<Feedback> feedBacks = _context.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Type)
                .ToList();
            var users = GetUsers(feedBacks);

            var feedbackViews = feedBacks
            .OrderByDescending(f => f.DateTime)
            .Select(f =>
                 new FeedbackViewModel
                 {
                     Id = f.Id,
                     FeedbackTypeId = f.TypeId,
                     Text = f.Text,
                     dateTime = f.DateTime,
                     IsViewed = f.IsViewed,
                     Reply = f.Reply,

                     UserId = f.User.Id,
                     UsersAvatarURL = f.User.AvatarURL,
                     UsersBirthDate = f.User.BirthDate,
                     UsersEmail = f.User.Email,
                     UsersFirstName = f.User.FirstName,
                     UsersSecondName = f.User.SecondName,
                     UsersThirdName = f.User.ThirdName,

                     FeedbackTypeName = f.Type.Name
                 }
            );

            if (queryParameters.SearchByName != null)
            {
                var toLowerSearchParameter = queryParameters.SearchByName.ToLower();

                feedbackViews = feedbackViews.Where(f =>
                f.Text.ToLower()
                .Contains(toLowerSearchParameter));
            }
            if (queryParameters.SearchByType != null)
            {
                feedbackViews = feedbackViews.Where(f =>
                f.FeedbackTypeId == queryParameters.SearchByType);
            }

            var feedbacks = PaginationHelper<FeedbackViewModel>
                .GetPageValues(feedbackViews.AsQueryable(), queryParameters.PageCount, queryParameters.Page).ToList();

            return new PageModel<List<FeedbackViewModel>>() { EntityAmount = feedbackViews.Count(), Entities = feedbacks };
        }

        public Feedback UpdateFeedback(FeedbackViewModel feedback)
        {
            Feedback FeedbackEntity = _context.Feedbacks.FirstOrDefault(x => x.Id == feedback.Id);

            FeedbackEntity.IsViewed = feedback.IsViewed;
            FeedbackEntity.Reply = feedback.Reply;
            FeedbackEntity.Text = feedback.Text;

            _context.SaveChanges();

            return FeedbackEntity;
        }

    }
}

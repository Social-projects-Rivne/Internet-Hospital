using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class FeedBackService : IFeedBackService
    {

        private ApplicationContext _context;

        public FeedBackService(ApplicationContext context)
        {
            _context = context;
        }

        public FeedBack FeedBackPOST(FeedBackModel model)
        {
            FeedBack feedback = new FeedBack
            {
                DateTime = DateTime.Now,
                Text = model.Text,
                UserId = model.UserId,
                FeedBackId = model.TypeId
            };
            _context.FeedBacks.AddAsync(feedback);
            _context.SaveChanges();
            return feedback;
        }

        public ICollection<FeedBackType> GetTypes()
        {
           
        }
    }
}

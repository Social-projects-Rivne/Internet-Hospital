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

        private ApplicationContext _context;

        public FeedBackService(ApplicationContext context)
        {
            _context = context;
        }

        public void FeedBackCreate(FeedBackModel model)
        {
            FeedBack feedback = new FeedBack
            {
                DateTime = DateTime.Now,
                Text = model.Text,
                UserId = model.UserId,
                FeedBackId = model.TypeId
            };
            _context.FeedBacks.Add(feedback);
            _context.SaveChanges();
        }

        public List<FeedBackType> GetTypes()
        {
            return _context.FeedBackTypes.ToList();
        }
    }
}

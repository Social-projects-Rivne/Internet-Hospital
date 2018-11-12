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

        public void FeedBackCreate(FeedBackCreationModel model, int userId)
        {
            FeedBack feedback = new FeedBack
            {
                DateTime = DateTime.Now,
                Text = model.Text,
                UserId = userId,
                TypeId = model.TypeId
            };

            _context.FeedBacks.Add(feedback);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FeedBackType> GetFeedBackTypes()
        {
            return _context.FeedBackTypes.ToList();
        }
    }
}

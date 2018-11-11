using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFeedBackService
    {
        void FeedBackCreate(FeedBackCreationModel model, int UserId);
        List<FeedBackType> GetFeedBackTypes();
    }
}
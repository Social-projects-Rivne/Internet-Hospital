using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFeedBackService
    {
        FeedBack FeedBackPOST(FeedBackModel model);
        IQueryable<FeedBackType> GetTypes();
    }
}
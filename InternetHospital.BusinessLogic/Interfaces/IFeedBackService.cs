using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFeedBackService
    {
        FeedBack FeedBackPOST(FeedBackModel model);
        ICollection<FeedBackType> GetTypes();
    }
}

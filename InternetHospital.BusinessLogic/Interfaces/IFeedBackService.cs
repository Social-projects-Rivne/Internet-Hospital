using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFeedBackService
    {
        FeedBack FeedBackCreate(FeedBackCreationModel model);
        (IEnumerable<FeedbackViewModel>, int count) GetFeedbackViewModels(FeedbackSearchParams queryParameters);
        IEnumerable<FeedBackType> GetFeedBackTypes();
        IEnumerable<UserModel> GetUsers();
    }
}
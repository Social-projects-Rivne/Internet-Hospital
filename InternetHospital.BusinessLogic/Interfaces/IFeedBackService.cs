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
        PageModel<List<FeedbackViewModel>> GetFeedbackViewModels(FeedbackSearchParams queryParameters);
        IEnumerable<FeedBackType> GetFeedBackTypes();
        IEnumerable<UserModel> GetUsers();
        IEnumerable<UserModel> GetUsers(IEnumerable<FeedBack> feedBacks);
        FeedBack UpdateFeedBack(FeedbackViewModel feedback);
    }
}
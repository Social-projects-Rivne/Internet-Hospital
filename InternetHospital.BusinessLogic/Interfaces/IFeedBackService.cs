using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFeedbackService
    {
        Feedback FeedbackCreate(FeedbackCreationModel model);
        PageModel<List<FeedbackViewModel>> GetFeedbackViewModels(FeedbackSearchParams queryParameters);
        IEnumerable<FeedbackType> GetFeedbackTypes();
        IEnumerable<UserModel> GetUsers();
        IEnumerable<UserModel> GetUsers(IEnumerable<Feedback> feedbacks);
        Feedback UpdateFeedback(FeedbackViewModel feedback);
    }
}
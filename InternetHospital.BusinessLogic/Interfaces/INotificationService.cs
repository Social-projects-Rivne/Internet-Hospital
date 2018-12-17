using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Notification;
using InternetHospital.DataAccess.Entities;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        PageModel<List<Notification>> GetMyNotifications(NotificationSearchModel model, int userId);
        bool ChangeReadStatus(int notificationId, int userId);
        bool AddNotification(int userId, string message);
        void NewMessageNotify(int id);
    }
}

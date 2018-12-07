using System;
using System.Collections.Generic;
using System.Linq;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Notification;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;

namespace InternetHospital.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationContext _context;

        public NotificationService(ApplicationContext context)
        {
            _context = context;
        }

        public PageModel<List<Notification>> GetMyNotifications(NotificationSearchModel model, int userId)
        {
            var notifications = _context.Notifications
                .OrderBy(n=>n.Date)
                .Where(n => n.RecepientId == userId);

            var notificationAmount = notifications.Count();
            var notificationResult = PaginationHelper<Notification>
                .GetPageValues(notifications, model.PageCount, model.Page)
                .ToList();

            return new PageModel<List<Notification>>()
                { EntityAmount = notificationAmount, Entities = notificationResult };

        }

        public bool ChangeReadStatus(int notificationId, int userId)
        {
            var notification = _context.Notifications
                .FirstOrDefault(n => n.RecepientId == userId && n.Id == notificationId);

            if (notification != null)
            {
                notification.IsRead = !notification.IsRead;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

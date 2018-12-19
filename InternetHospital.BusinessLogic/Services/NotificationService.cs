using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Hubs;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Notification;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.SignalR;

namespace InternetHospital.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private const string NOTIFY_METHOD = "Notify";
        private const string ONLOAD_METHOD = "OnLoad";

        public NotificationService(ApplicationContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public PageModel<List<Notification>> GetMyNotifications(NotificationSearchModel model, int userId)
        {
            var notifications = _context.Notifications
                .OrderByDescending(n=>n.Date)
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

        private int GetUnreadNotificationsCount(int userId)
        {
            var count = _context.Notifications.Count(n => n.RecepientId == userId 
                                                          && !n.IsRead);
            return count;
        }

        public bool AddNotification(int userId, string message)
        {
            Notification notification = new Notification
            {
                RecepientId = userId,
                IsRead = false,
                Message = message,
                Date = DateTime.Now
            };

            try
            {
                _context.Notifications.Add(notification);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void NewMessageNotify(int id)
        {
            _hubContext.Clients.User(id.ToString())
                   .SendAsync(NOTIFY_METHOD, GetUnreadNotificationsCount(id));
        }

        public async Task OnLoad(int id)
        {
            await _hubContext.Clients.User(id.ToString())
                   .SendAsync(ONLOAD_METHOD, GetUnreadNotificationsCount(id));
        }
    }
}

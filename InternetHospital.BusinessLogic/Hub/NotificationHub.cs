using InternetHospital.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly INotificationService _notificationService;

        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                _notificationService.NewMessageNotify(int.Parse(userId));
            }
        }
    }
}

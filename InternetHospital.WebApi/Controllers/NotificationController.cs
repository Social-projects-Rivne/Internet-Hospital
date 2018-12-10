using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models.Notification;
using InternetHospital.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        IHubContext<NotificationHub> _hubContext;

        public NotificationController(INotificationService notificationService, IHubContext<NotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] NotificationSearchModel model)
        {
            if (!int.TryParse(User.Identity.Name, out var userId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var myNotifications = _notificationService.GetMyNotifications(model, userId);

            return Ok(myNotifications);
        }

        [Authorize]
        [HttpPatch("change")]
        public IActionResult ChangeReadStatus([FromBody]int id)
        {
            if (!int.TryParse(User.Identity.Name, out var userId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var status = _notificationService.ChangeReadStatus(id, userId);

            if (status)
            {
                _hubContext.Clients.User(User.Identity.Name)
                    .SendAsync("OnLoad", _notificationService.GetUnreadNotificationsCount(userId));
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("test")]
        public IActionResult test(int who, string message)
        {
           var status = _notificationService.AddNotification(who, message);
            if (status)
            {
                _hubContext.Clients.User(who.ToString())
                    .SendAsync("Notify", _notificationService.GetUnreadNotificationsCount(who));
                return Ok();
            }

            return BadRequest();
        }
    }
}
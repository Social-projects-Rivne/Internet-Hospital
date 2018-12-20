using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models.Notification;
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

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
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
                _notificationService.OnLoad(userId);
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("checkall")]
        public IActionResult CheckAll()
        {
            if (!int.TryParse(User.Identity.Name, out var userId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var status = _notificationService.CheckAllNotifications(userId);
            if (status)
            {
                _notificationService.OnLoad(userId);
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles ="Admin, Moderator")]
        [HttpPost("send")]
        public IActionResult Send([FromBody] MessageModel model)
        {
            var status = _notificationService.AddNotification(model.Recepient, model.Message);
            if (status)
            {
                _notificationService.NewMessageNotify(model.Recepient);
                return Ok();
            }

            return BadRequest();
        }
    }
}

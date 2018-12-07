using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get([FromRoute] NotificationSearchModel model)
        {
            if (!int.TryParse(User.Identity.Name, out var userId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var myNotifications = _notificationService.GetMyNotifications(model, userId);

            return Ok(myNotifications);
        }

        public class Test
        {
            public int Id { get; set; }
        }


        [Authorize]
        [HttpPatch("change")]
        public IActionResult ChangeReadStatus([FromBody]Test model)
        {
            if (!int.TryParse(User.Identity.Name, out var userId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var status = _notificationService.ChangeReadStatus(model.Id, userId);

            return status ? (IActionResult)Ok() : BadRequest();
        }
    }
}
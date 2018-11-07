using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpGet]
        public IActionResult GetAppointments()
        {
            if (int.TryParse(User.Identity.Name, out int doctorId))
            {
                var myAppointments = _appointmentService.GetMyAppointments(doctorId).ToList();

                return Ok(new { appointments=myAppointments });
            }
            else
            {
                return BadRequest(new { message = "Wrong claims" });
            }
        }

        [HttpGet("getavailable")]
        public IActionResult GetAvailableAppointments([FromQuery] int doctorId)
        {
            var availableAppointments = _appointmentService.GetAvailableAppointments(doctorId).ToList();

            return Ok(availableAppointments);
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("create")]
        public IActionResult AddAppointment([FromBody] AppointmentCreationModel creationModel)
        {
            if (int.TryParse(User.Identity.Name, out var doctorId))
            {
                var (status, message) = _appointmentService.AddAppointment(creationModel,doctorId);

                if (status)
                {
                    return Ok(new { message });
                }
                else
                {
                    return BadRequest(new { message });
                }
            }
            else
            {
                return BadRequest(new { message = "Wrong claims" });
            }
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("cancel")]
        public IActionResult CancelAppointment([FromBody] AppointmentEditingModel model)
        {
            if (int.TryParse(User.Identity.Name, out var doctorId))
            {
                var (status, message) = _appointmentService.CancelAppointment(model.Id, doctorId);

                if (status)
                {
                    return Ok(new { message });
                }
                else
                {
                    return BadRequest(new { message });
                }
            }
            else
            {
                return BadRequest(new { message = "Wrong claims" });
            }
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("delete")]
        public IActionResult DeleteAppointment([FromBody] AppointmentEditingModel model)
        {
            if (int.TryParse(User.Identity.Name, out var doctorId))
            {
                var (status, message) = _appointmentService.DeleteAppointment(model.Id, doctorId);

                if (status)
                {
                    return Ok(new { message });
                }
                else
                {
                    return BadRequest(new { message });
                }
            }
            else
            {
                return BadRequest(new { message = "Wrong claims" });
            }
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("finish")]
        public IActionResult FinishAppointment([FromBody] AppointmentEditingModel model)
        {
            if (int.TryParse(User.Identity.Name, out var doctorId))
            {
                var (status, message) = _appointmentService.FinishAppointment(model.Id, doctorId);

                if (status)
                {
                    return Ok(new { message });
                }
                else
                {
                    return BadRequest(new { message });
                }
            }
            else
            {
                return BadRequest(new { message = "Wrong claims" });
            }
        }
    }
}
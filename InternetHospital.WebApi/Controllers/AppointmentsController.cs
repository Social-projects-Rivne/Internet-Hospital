using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAppointments()
        {
            if (int.TryParse(User.Identity.Name, out int doctorId))
            {
                var doctorAppointment = _appointmentService.GetDoctorAppointments(doctorId);

                return Ok(doctorAppointment);
            }
            else
            {
                return BadRequest(new { message = "Wrong claims" });
            }
        }

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
    }
}
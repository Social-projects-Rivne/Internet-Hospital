﻿using InternetHospital.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using InternetHospital.BusinessLogic.Models.Appointment;

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
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var myAppointments = _appointmentService.GetMyAppointments(doctorId).ToList();

            return Ok(new { appointments = myAppointments });
        }

        [HttpGet("available")]
        public IActionResult GetAvailableAppointments([FromQuery] AppointmentSearchModel parameters)
        {
            var (appointments, quantity) = _appointmentService.GetAvailableAppointments(parameters);

            return Ok(
                new
                {
                    appointments,
                    quantity
                }
            );
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("create")]
        public IActionResult AddAppointment([FromBody] AppointmentCreationModel creationModel)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var status = _appointmentService.AddAppointment(creationModel, doctorId);

            return status ? (IActionResult)Ok() : BadRequest();
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("cancel")]
        public IActionResult CancelAppointment([FromBody] AppointmentEditingModel model)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var (status, message) = _appointmentService.CancelAppointment(model.Id, doctorId);

            return status ? (IActionResult)Ok(new { message }) : BadRequest(new { message });
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpDelete("delete")]
        public IActionResult DeleteAppointment([FromBody] AppointmentEditingModel model)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var (status, message) = _appointmentService.DeleteAppointment(model.Id, doctorId);

            return status ? (IActionResult)Ok(new { message }) : BadRequest(new { message });
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("finish")]
        public IActionResult FinishAppointment([FromBody] AppointmentEditingModel model)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var (status, message) = _appointmentService.FinishAppointment(model.Id, doctorId);

            return status ? (IActionResult)Ok(new { message }) : BadRequest(new { message });
        }
    }
}
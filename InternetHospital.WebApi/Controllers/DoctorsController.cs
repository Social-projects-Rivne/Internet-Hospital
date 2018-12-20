using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Appointment;
using InternetHospital.BusinessLogic.Models.DoctorBlackList;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService service)
        {
            _doctorService = service;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get([FromRoute] int id)
        {
            var doctor = _doctorService.Get(id);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            else
            {
                return NotFound(new { message = "Couldn't find such doctor" });
            }
        }

        [HttpPut("updateAvatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar([FromForm(Name = "Image")]IFormFile file)
        {
            var doctorId = User.Identity?.Name;
            bool result = await _doctorService.UpdateDoctorAvatar(doctorId, file);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("getAvatar")]
        [Authorize]
        public async Task<IActionResult> GetAvatar()
        {
            var doctorId = User.Identity?.Name;
            string avatarURL = await _doctorService.GetDoctorAvatar(doctorId);
            return Ok(new
            {
                avatarURL
            });
        }

        // GET: api/Doctors
        [HttpGet]
        public IActionResult GetDoctors([FromQuery] DoctorSearchParameters queryParameters)
        {
            var (doctors, count) = _doctorService.GetFilteredDoctors(queryParameters);

            return Ok(
                new
                {
                    doctors,
                    totalDoctors = count
                }
              );
        }

        [HttpGet("specializations")]
        public IEnumerable<SpecializationModel> GetSpecializations()
        {
            return _doctorService.GetAvailableSpecialization();
        }

        [HttpPut("update")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateDoctor([FromForm(Name = "PassportURL")]IFormFileCollection passport,
            [FromForm(Name = "DiplomaURL")]IFormFileCollection diploma,
            [FromForm(Name = "LicenseURL")]IFormFileCollection license)
        {

            var doctorModel = new DoctorProfileModel
            {
                FirstName = Request.Form["FirstName"],
                SecondName = Request.Form["SecondName"],
                ThirdName = Request.Form["ThirdName"],
                PhoneNumber = Request.Form["PhoneNumber"],
                BirthDate = DateTime.Parse(Request.Form["BirthDate"]),
                Address = Request.Form["Address"],
                Specialization = Request.Form["Specialization"],
            };

            TryValidateModel(doctorModel);

            if (ModelState.IsValid)
            {
                if (int.TryParse(User.Identity.Name, out int userId))
                {
                    var result = await _doctorService.UpdateDoctorInfo(doctorModel, userId, passport, diploma, license);
                    return result ? (IActionResult)Ok() : BadRequest(new { message = "Error during updating!" });
                }
                else
                {
                    return BadRequest(new { message = "Error with user ID!" });
                }
            }
            return BadRequest();
        }

        [HttpGet("getProfile")]
        [Authorize(Roles = "Doctor")]
        public IActionResult GetDoctorProfile()
        {
            if (!int.TryParse(User.Identity.Name, out int userId))
            {
                return BadRequest();
            }

            var doctor = _doctorService.GetDoctorProfile(userId);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            return BadRequest(new { message = "Cannot get profile data!" });
        }

        [HttpPost("illnesshistory")]
        [Authorize(Policy = "ApprovedDoctors")]
        public IActionResult FillHistory([FromBody] IllnessHistoryModel illnessHistory)
        {
            var (status, message) = _doctorService.FillIllnessHistory(illnessHistory);

            return status ? (IActionResult)Ok() : BadRequest(new { message });
        }

        [HttpGet("GetPatientInfo")]
        [Authorize(Policy = "ApprovedDoctors")]
        public IActionResult GetPatientInfo([FromQuery] int userId)
        {
            if (!int.TryParse(User.Identity.Name, out int doctorId))
            {
                return BadRequest();
            }

            var patient = _doctorService.GetPatientInfo(userId, doctorId);

            return patient != null ? (IActionResult)Ok(patient) : BadRequest();
        }

        [HttpGet("GetPatientIllnessHistory")]
        [Authorize(Policy = "ApprovedDoctors")]
        public IActionResult GetPatientIllnessHistory([FromQuery] IllnessHistorySearchModel queryParameters)
        {
            if (!int.TryParse(User.Identity.Name, out int doctorId))
            {
                return BadRequest();
            }

            var illnessHistories = _doctorService.GetPatientIllnessHistory(queryParameters, doctorId);

            return illnessHistories != null ?
                (IActionResult)Ok(new
                {
                    illnesses = illnessHistories.Entities,
                    amount = illnessHistories.EntityAmount
                })
            : BadRequest();
        }

        [HttpGet("mypatients")]
        [Authorize(Policy = "ApprovedDoctors")]
        public IActionResult GetMyPatients([FromQuery] MyPatientsSearchParameters patientsSearch)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var pats = _doctorService.GetMyPatients(doctorId, patientsSearch);

            return Ok(pats);
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("addtoblacklist")]
        public IActionResult AddToBlackList([FromBody] AddToBlackListModel creationModel)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var status = _doctorService.AddToBlackList(creationModel, doctorId);

            return status ? (IActionResult)Ok() : BadRequest();
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpGet("myblacklist")]
        public IActionResult GetMyBlackList([FromQuery] MyPatientsSearchParameters creationModel)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var blackList = _doctorService.GetBlackList(doctorId, creationModel);

            return Ok(blackList);
        }

        [Authorize(Policy = "ApprovedDoctors")]
        [HttpPost("removefromblacklist")]
        public IActionResult RemoveFromBlackList([FromBody] RemoveFromBlackListModel creationModel)
        {
            if (!int.TryParse(User.Identity.Name, out var doctorId))
            {
                return BadRequest(new { message = "Wrong claims" });
            }

            var status = _doctorService.RemoveFromBlackList(creationModel.id, doctorId);

            return status ? (IActionResult)Ok() : BadRequest();
        }
    }
}

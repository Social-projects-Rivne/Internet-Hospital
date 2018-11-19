using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
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

            var doctor =  _doctorService.GetDoctorProfile(userId);
            if (doctor != null)
            {
                return Ok(doctor);

            }
            return BadRequest(new { message = "Cannot get profile data!" });
        }
    }
}

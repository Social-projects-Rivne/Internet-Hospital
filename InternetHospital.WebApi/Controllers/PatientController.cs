using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUploadingFiles _uploadingFiles;
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public PatientController(IUploadingFiles uploadingFiles, ApplicationContext context, 
            UserManager<User> userManager)
        {
            _uploadingFiles = uploadingFiles;
            _context = context;
            _userManager = userManager;
        }

        [HttpPut("updateAvatar")]
        [Authorize]
        public async Task<ActionResult> UpdateAvatar([FromForm(Name = "Image")]IFormFile file)
        {
            var patientId = User.Identity?.Name;
            if (patientId != null && file != null)
            {
                var patient = await _userManager.FindByIdAsync(patientId);                
                await _uploadingFiles.UploadAvatar(file, patient);
                return Ok();
            }
            return BadRequest(new { message = "Cannot change avatar!" });
        }

        [HttpGet("getAvatar")]
        [Authorize]
        public async Task<IActionResult> GetAvatar()
        {
            var patientId = User.Identity?.Name;
            if (patientId != null)
            {
                var patient = await _userManager.FindByIdAsync(patientId);
                return Ok(new
                {
                    patient.AvatarURL
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult> UpdatePatientProfile([FromForm(Name = "PassportURL")]IFormFileCollection files)
        {
            if (files == null)
            {
                ModelState.AddModelError("Files", "Files didn't upload");
            }
            var patientModel = new PatientModel
            {
                FirstName = Request.Form["FirstName"],
                SecondName = Request.Form["SecondName"],
                ThirdName = Request.Form["ThirdName"],
                PhoneNumber = Request.Form["PhoneNumber"],
                BirthDate = Request.Form["BirthDate"]
            };

            TryValidateModel(patientModel);

            if (ModelState.IsValid)
            {
                var patient = _context.Users.FirstOrDefault(p => p.Id == int.Parse(User.Identity.Name));
                if (patient == null)
                {
                    return BadRequest();
                }
                _context.Update(patient);

                patient.FirstName = patientModel.FirstName;
                patient.SecondName = patientModel.SecondName;
                patient.ThirdName = patientModel.ThirdName;
                patient.PhoneNumber = patientModel.PhoneNumber;
                patient.BirthDate = DateTime.ParseExact(patientModel.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var userWithPassport = await _uploadingFiles.UploadPassport(files, patient);
                if (userWithPassport != null)
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "Error during files uploading!" });
                }
            }
            return BadRequest();
        } 
    }
}
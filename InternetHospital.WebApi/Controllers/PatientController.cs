using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
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
        private readonly UserManager<User> _userManager;
        private readonly IPatientService _patientService;
        private readonly IFilesService _uploadingFiles;

        public PatientController(UserManager<User> userManager, IPatientService patientService, IFilesService uploadingFiles)
        {
            _userManager = userManager;
            _uploadingFiles = uploadingFiles;
            _patientService = patientService;
        }

        
        [HttpGet("GetHistories")]
        public async Task<IActionResult> GetIllnessHistories([FromQuery] IllnessHistorySearchModel queryParameters)
        {
            var patientId = User.Identity?.Name;
            var (histories, count) = await _patientService.GetFilteredHistories(queryParameters, patientId);

            return Ok(
                new
                {
                    histories,
                    totalHistories = count,
                }
              );
        }

        [HttpPut("updateAvatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar([FromForm(Name = "Image")]IFormFile file)
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
        public async Task<IActionResult> UpdatePatientProfile([FromForm(Name = "PassportURL")]IFormFileCollection files)
        {
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
                if (int.TryParse(User.Identity.Name, out int userId))
                {
                    var result = await _patientService.UpdatePatientInfo(patientModel, userId, files);
                    return result ? (IActionResult)Ok() : BadRequest(new { message = "Error during updating!" });
                }
                else
                {
                    return BadRequest(new { message = "Error with user ID!" });
                }
            }
            return BadRequest();
        }

        [HttpGet("GetDetailedProfile")]
        public async Task<IActionResult> GetPatient()
        {
            var patientId = User.Identity?.Name;
            if (patientId != null)
            {
                var patient = await _userManager.FindByIdAsync(patientId);
                var returnPatient = await _patientService.Get(patient.Id);
                if (returnPatient != null)
                {
                    return Ok(returnPatient);
                }
            }
            return BadRequest(new { message = "Couldnt find a patient" });
        }
    }
}
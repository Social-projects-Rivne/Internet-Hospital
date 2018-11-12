using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
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
        private IUploadingFiles _uploadingFiles;
        private ApplicationContext _context;
        private UserManager<User> _userManager;

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
                return Ok(new
                {
                    message = "Avatar was updated!"
                });
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

    }
}
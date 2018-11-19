using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IModeratorService _moderatorService;
        private readonly IMailService _mailService;

        public ModeratorController(UserManager<User> userManager, IModeratorService moderatorService, IMailService mailService)
        {
            _userManager = userManager;
            _moderatorService = moderatorService;
            _mailService = mailService;
        }

        // GET: api/Moderator
        [HttpGet]
        public IActionResult GetModerators([FromQuery] ModeratorSearchParameters moderatorsSearch)
        {
            var mods = _moderatorService.GetFilteredModerators(moderatorsSearch);
            return Ok(mods);
        }

        [HttpPost]
        public async Task<IActionResult> PostModeratorAsync([FromBody] ModeratorCreatingModel moderatorCreatingModel)
        {
            var moder = await _moderatorService.CreateModeratorAsync(moderatorCreatingModel);
            if (moder.Item1 != null)
            {
                string callbackUrl = await GenerateConfirmationLinkAsync(moder.Item1);
                await _mailService.SendMsgToEmail(moder.Item1.Email, "Confirm Your account, please",
                    $"Confirm registration following the link: <a href='{callbackUrl}'>Confirm email NOW</a>");
                return Ok(new { message = moder.Item2 });
            }
            return BadRequest(new { message = moder.Item2 });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModeratorAsync(int id)
        {
            var result = await _moderatorService.DeleteAsync(id);
            if (result.Item1)
            {
                return Ok(new { message = result.Item2 });
            }
            return BadRequest(new { message = result.Item2 });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteModeratorAsync([FromQuery]int[] ids)
        {
            var result = await _moderatorService.DeleteAsync(ids);
            return Ok(result);
        }

        private async Task<string> GenerateConfirmationLinkAsync(User user)
        {
            var codes = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Signup",
                new { userId = user.Id, code = codes },
                protocol: HttpContext.Request.Scheme
            );
            return callbackUrl;
        }
    }
}

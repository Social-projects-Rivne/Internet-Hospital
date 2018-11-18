using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService ms)
        {
            _moderatorService = ms;
        }

        // GET: api/Moderator
        [HttpGet]
        public IActionResult GetModerators([FromQuery] ModeratorSearchParameters moderatorsSearch)
        {
            var mods = _moderatorService.GetFilteredModerators(moderatorsSearch);
            return Ok(mods);
        }

    }
}

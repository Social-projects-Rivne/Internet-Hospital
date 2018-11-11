using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {

        private IFeedBackService _feedBackService;
        private UserManager<User> _userManager;

        public FeedBackController(IFeedBackService feedBackService,
             UserManager<User> userManager)
        {
            _feedBackService = feedBackService;
            _userManager = userManager;
        }

        [HttpPost("Create")]
        public IActionResult CreateFeedBack([FromBody]FeedBackCreationModel feedBackCreationModel)
        {

            int userId;
            Int32.TryParse(User.Identity.Name,out userId);

            if (feedBackCreationModel != null)
            {
                _feedBackService.FeedBackCreate(feedBackCreationModel, userId);
                return Ok();
            }
            else
            {
                return NotFound(new { message = "The form isn't valid or there is no currently logined users" });
            }
        }

        [HttpGet("feedbacktypes")]
        public ICollection<FeedBackType> GetFeedBacks()
        {
            return _feedBackService.GetFeedBackTypes();
        }

    }
}

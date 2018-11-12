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
            Int32.TryParse(User.Identity.Name, out userId);

            if (feedBackCreationModel != null)
            {
                if (feedBackCreationModel.Text.Length <= 10)
                    return NotFound(new { message = "The text not in correct format" });
                if (feedBackCreationModel.TypeId == 0)
                    return NotFound(new { message = "Feedback format is invalid" });
                else
                {
                    _feedBackService.FeedBackCreate(feedBackCreationModel, userId);
                    return Ok(new { message = "Thank you for your feedback" });
                }
            }
            else
            {
              //  ModelState.AddModelError("", "Wrong form!");
                return BadRequest(new { message = "Wrong form!" });
            }
        }

        [HttpGet("feedbacktypes")]
        public ICollection<FeedBackType> GetFeedBacks()
        {
            return _feedBackService.GetFeedBackTypes();
        }

    }
}

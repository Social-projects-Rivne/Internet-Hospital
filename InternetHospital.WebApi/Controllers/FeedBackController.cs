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
        private readonly IFeedBackService _feedBackService;
        private readonly UserManager<User> _userManager;

        public FeedBackController(IFeedBackService feedBackService,
             UserManager<User> userManager)
        {
            _feedBackService = feedBackService;
            _userManager = userManager;
        }

        [HttpPost("Create")]
        public IActionResult CreateFeedBack([FromBody]FeedBackCreationModel feedBackCreationModel)
        {
            if (int.TryParse(User.Identity.Name, out int userid)
                || feedBackCreationModel != null)
            {
                feedBackCreationModel.UserId = userid;
                var feedback = _feedBackService.FeedBackCreate(feedBackCreationModel);
                return feedback == null ? (IActionResult)BadRequest(new { message = "Not valid feedback" }) : Ok();
            }
            else
            {
                return NotFound(new { message = "Form not valid" });
            }
        }

        [HttpGet("feedbacktypes")]
        public IActionResult GetFeedBackTypes()
        {
            return Ok(_feedBackService.GetFeedBackTypes());
        }

        [HttpGet("getinvolvedusers")]
        public IActionResult GedInvolvedUsers()
        {
            return _feedBackService.GetUsers() == null ? (IActionResult)BadRequest() : Ok(_feedBackService.GetUsers());
        }

        [HttpGet("getviewfeedbacks")]
        public IActionResult GetViewFeedbacks([FromQuery]FeedbackSearchParams queryParameters)
        {
            var result = _feedBackService.GetFeedbackViewModels(queryParameters);

            return Ok(
                new
                {
                    feedbacks = result.Entities,
                    quantity = result.EntityAmount
                });
        }

        [HttpPut("updatefeedback")]
        public IActionResult UpdateFeedback([FromBody]FeedbackViewModel feedback)
        {
            var reply = _feedBackService.UpdateFeedBack(feedback);
            return reply == null ? (IActionResult)BadRequest() : Ok();
        }

    }
}

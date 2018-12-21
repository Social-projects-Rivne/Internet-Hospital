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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly UserManager<User> _userManager;

        public FeedbackController(IFeedbackService feedBackService,
             UserManager<User> userManager)
        {
            _feedbackService = feedBackService;
            _userManager = userManager;
        }

        [HttpPost("Create")]
        public IActionResult CreateFeedBack([FromBody]FeedbackCreationModel feedbackCreationModel)
        {
            if (int.TryParse(User.Identity.Name, out int userid)
                || feedbackCreationModel != null)
            {
                feedbackCreationModel.UserId = userid;
                var feedback = _feedbackService.FeedbackCreate(feedbackCreationModel);
                return feedback == null ? (IActionResult)BadRequest(new { message = "Not valid feedback" }) : Ok();
            }
            else
            {
                return NotFound(new { message = "Form is not valid" });
            }
        }

        [HttpGet("feedbacktypes")]
        public IActionResult GetFeedBackTypes()
        {
            return Ok(_feedbackService.GetFeedbackTypes());
        }

        [HttpGet("getinvolvedusers")]
        public IActionResult GedInvolvedUsers()
        {
            return _feedbackService.GetUsers() == null ? (IActionResult)BadRequest() : Ok(_feedbackService.GetUsers());
        }

        [HttpGet("getviewfeedbacks")]
        public IActionResult GetViewFeedbacks([FromQuery]FeedbackSearchParams queryParameters)
        {
            var result = _feedbackService.GetFeedbackViewModels(queryParameters);

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
            var reply = _feedbackService.UpdateFeedback(feedback);
            return reply == null ? (IActionResult)BadRequest() : Ok();
        }

    }
}

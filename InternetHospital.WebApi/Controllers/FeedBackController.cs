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

        FeedBackController(IFeedBackService feedBackService,
             UserManager<User> userManager)
        {
            _feedBackService = feedBackService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            var FeedBackModel = new FeedBackModel
            {
                TypeId = Convert.ToInt32(Request.Form["Type"]),
                Text = Request.Form["Text"],
                UserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id
            };

            if (
                !String.IsNullOrEmpty(FeedBackModel.Text)
                && !String.IsNullOrEmpty(FeedBackModel.TypeId.ToString())
                && !String.IsNullOrEmpty(FeedBackModel.UserId.ToString())
               )
            {
                _feedBackService.FeedBackCreate(FeedBackModel);
                return Ok();
            }
            else
            {
                return NotFound(new { message = "The form isn't valid or there is no currently logined users" });
            }

        }


    }
}

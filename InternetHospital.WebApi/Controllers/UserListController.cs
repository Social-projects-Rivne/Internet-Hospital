using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListController : ControllerBase
    {
        private readonly IUserListService _userListService;

        public UserListController(IUserListService userListService)
        {
            _userListService = userListService;
        }

        // GET: api/userlist 
        [HttpGet]
        public IActionResult Get()
        {
            var Users = _userListService.GetUsers();
            return Ok(Users);
        }

        [HttpGet("getparams")]
        public IActionResult GetDoctors([FromQuery]UserSearchParameters queryParameters)
        {
            var (users, quantity) = _userListService.FilteredUsers(queryParameters);

            return Ok(
                new {
                    users,
                    Count = quantity
                });
        }

    }
}

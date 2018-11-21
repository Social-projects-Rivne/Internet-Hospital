using InternetHospital.BusinessLogic.Interfaces;
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
    }
}

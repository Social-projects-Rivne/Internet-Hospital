using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Services;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListController : ControllerBase
    {

        private readonly IUserListService _userListService;

        UserListController(IUserListService userListService)
        {
            _userListService = userListService;
        }

        // GET: api/UserList
        [HttpGet]
        public ICollection<User> Get()
        {
            return _userListService.GetUsers();
        }

        // PUT: api/UserList/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}

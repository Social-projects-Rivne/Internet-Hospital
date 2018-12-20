using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("getPatientToDoctor")]
        public IActionResult GetPatientToDoctorRequests([FromQuery] PageConfig pageConfig)
        {
            var patients = _requestService.GetRequestsPatientToDoctor(pageConfig);
            return Ok(patients);
        }

        [HttpPost("handlePatientToDoctor")]
        public async Task<IActionResult> HandlePatientToDoctorRequest(RequestResultModel requestResultModel) //SomeResutModel: id and isApproved - fields
        {
            var result = await _requestService.HandlePatientToDoctorRequest(requestResultModel.Id, requestResultModel.IsApproved); // CHANGE HERE
            if (result.Item1)
            {
                return Ok(new { message = result.Item2 });
            }
            return BadRequest(new { message = result.Item2 });
        }
    }
}
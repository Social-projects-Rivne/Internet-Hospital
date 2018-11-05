using System.Collections.Generic;
using System.Linq;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ApplicationContext _context;
        public DoctorsController(ApplicationContext context, IDoctorService service)
        {
            _context = context;
            _doctorService = service;
        }

        // GET: api/Doctors
        [HttpGet]
        public IActionResult GetDoctors([FromQuery] DoctorSearchParameters queryParameters)
        {
            var (doctors, count) = _doctorService.GetAll(queryParameters);
            List<DoctorModel> allDoctors = doctors.OrderBy(x => x.SecondName).ToList();
            return Ok(
                new
                {
                    doctors = allDoctors,
                    totalDoctors = count
                }
              );
        }

        [HttpGet("specializations")]
        public ICollection<SpecializationModel> GetSpecializations()
        {
            var specizalizations = _context.Specializations.Where(s => s.Doctors.Count > 0).OrderBy(s => s).Select(s => new SpecializationModel { Id = s.Id, Specialization = s.Name }).ToList();
            return specizalizations;
        }
    }
}

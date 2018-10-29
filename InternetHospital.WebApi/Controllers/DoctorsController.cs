using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private ApplicationContext _context;
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
            List<DoctorsDto> allDoctors = doctors.OrderBy(x => x.SecondName).ToList();
            return Ok(
                new
                {
                    doctors = allDoctors,
                    totalDoctors = count
                }
              );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private ApplicationContext _context;
        public DoctorsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public IQueryable<DoctorsDto> GetDoctors()
        {
            var doctors = from b in _context.Doctors select new DoctorsDto()
            {
                Id = b.UserId,
                FirstName = b.User.FirstName,
                SecondName = b.User.SecondName,
                ThirdName = b.User.ThirdName,
                SpecializationName = b.Specialization.Name
            };
            //IEnumerable<Doctor> DoctorInfo = _context.Doctors
            //        .Include(d => d.User)
            //        .Include(d => d.Specialization);
            return doctors;
        }

        // GET: api/Doctors/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Doctors
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using DoctorApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace DoctorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        public SpecializationController(DoctorAppDbContext context)
        {
            _context = context;
        }

        // Get All Specializations
        [HttpGet("Data")]
        public IActionResult GetSpecializations()
        {
            var specializations = _context.Specializations.ToList();

            return StatusCode(200, specializations);
        }

        // Search Specialization By Name
        [HttpGet("Search")]
        public IActionResult SearchSpecialization(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return StatusCode(400, "Please Enter a Specialization Name");
            }

            var specialization = _context.Specializations
                                         .Where(x => x.Name.Contains(name))
                                         .ToList();

            if (specialization.Count == 0)
            {
                return StatusCode(404, "Specialization Not Found");
            }

            return StatusCode(200, specialization);
        }

        // Get Doctors By Specialization
        [HttpGet("Doctors")]
        public IActionResult GetDoctorsBySpecialization(int specializationId)
        {
            var doctors = _context.DoctorSpecializations
                .Include(x => x.Doctor)
                .ThenInclude(x => x.User)
                .Where(x => x.SpecializationId == specializationId)
                .Select(x => new
                {
                    Name = x.Doctor.User.Name,
                    HourRate = x.Doctor.HourRate
                })
                .ToList();

            if (doctors.Count == 0)
            {
                return StatusCode(404, "No Doctors Found");
            }

            return StatusCode(200, doctors);
        }
    }
}
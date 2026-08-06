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

      
        [HttpGet("Data")]
        public IActionResult GetSpecializations()
        {
            var specializations = _context.Specializations.ToList();

            return StatusCode(200, specializations);
        }

       
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
    }
}
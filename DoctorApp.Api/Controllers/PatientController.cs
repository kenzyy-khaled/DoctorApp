using DoctorApp.Api.RequestBody;
using DoctorApp.Data;
using DoctorApp.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        public PatientController(DoctorAppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register/Patient")]
        public IActionResult RegisterPatient([FromBody] RegisterPatientRequestBody requestBody)
        {
            
            if (requestBody.Password != requestBody.ConfirmPassword)
            {
                return StatusCode(400, "Password is not similar");
            }

            
            var newUser = new User
            {
                Name = requestBody.FullName,
                Email = requestBody.Email,
                Password = requestBody.Password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

           
            var newPatient = new Patient
            {
                UserId = newUser.Id
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();

           
            newUser.PatientId = newPatient.Id;

            _context.Users.Update(newUser);
            _context.SaveChanges();

            return StatusCode(200, "Register Completed Successfully");
        }
    }
}
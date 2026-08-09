using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoctorApp.Api.RequestBody;
using DoctorApp.Data;
using DoctorApp.Entities.Users;
using System.Linq;

namespace DoctorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        public DoctorController(DoctorAppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register/Doctor")]
        public IActionResult RegisterDoctor([FromBody] RegisterDoctorRequestBody requestBody)
        {
            if (requestBody == null)
            {
                return StatusCode(400, "Request Body is Empty");
            }

            if (string.IsNullOrWhiteSpace(requestBody.FullName))
            {
                return StatusCode(400, "Full Name is Required");
            }

            if (string.IsNullOrWhiteSpace(requestBody.Email))
            {
                return StatusCode(400, "Email is Required");
            }

            if (string.IsNullOrWhiteSpace(requestBody.Password))
            {
                return StatusCode(400, "Password is Required");
            }

            if (string.IsNullOrWhiteSpace(requestBody.ConfirmPassword))
            {
                return StatusCode(400, "Confirm Password is Required");
            }

            if (requestBody.Password != requestBody.ConfirmPassword)
            {
                return StatusCode(400, "Password is not similar");
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == requestBody.Email);

            if (user != null)
            {
                return StatusCode(400, "Email already exists");
            }

            var newUser = new User
            {
                Name = requestBody.FullName,
                Email = requestBody.Email,
                PhoneNumber = requestBody.PhoneNumber,
                Age = requestBody.Age,
                Gender = requestBody.Gender,
                Password = requestBody.Password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var newDoctor = new Doctor
            {
                UserId = newUser.Id,
                Image = requestBody.Image,
                DOB = requestBody.DOB,
                YearOfExperience = requestBody.YearOfExperience,
                Bio = requestBody.Bio
            };

            _context.Doctors.Add(newDoctor);
            _context.SaveChanges();

            newUser.DoctorId = newDoctor.UserId;

            _context.Users.Update(newUser);
            _context.SaveChanges();

            return StatusCode(200, "Register Completed Successfully");
        }

        [HttpGet("Profile")]
        public IActionResult GetDoctorProfile(int userId)
        {
            var doctor = _context.Doctors
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    UserId = x.UserId,
                    Name = x.User.Name,
                    Email = x.User.Email,
                    Image = x.Image,
                    DOB = x.DOB,
                    YearOfExperience = x.YearOfExperience,
                    LicenseNumber = x.LicenseNumber,
                    Bio = x.Bio,
                    HourRate = x.HourRate
                })
                .FirstOrDefault();

            if (doctor == null)
            {
                return StatusCode(404, "Doctor Not Found");
            }

            return StatusCode(200, doctor);
        }

        [HttpGet("Search")]
        public IActionResult SearchDoctor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return StatusCode(400, "Please Enter Doctor Name");
            }

            var doctors = _context.Doctors
                .Where(x => x.User.Name.Contains(name))
                .Select(x => new
                {
                    UserId = x.UserId,
                    Name = x.User.Name,
                    HourRate = x.HourRate
                })
                .ToList();

            if (doctors.Count == 0)
            {
                return StatusCode(404, "Doctor Not Found");
            }

            return StatusCode(200, doctors);
        }
    }
}
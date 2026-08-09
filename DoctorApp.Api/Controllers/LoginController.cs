using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoctorApp.Api.RequestBody;
using DoctorApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DoctorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        public LoginController(DoctorAppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestBody requestBody)
        {
            if (requestBody == null)
            {
                return StatusCode(400, "Request Body is Empty");
            }

            if (string.IsNullOrWhiteSpace(requestBody.Email))
            {
                return StatusCode(400, "Email is Required");
            }

            if (string.IsNullOrWhiteSpace(requestBody.Password))
            {
                return StatusCode(400, "Password is Required");
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == requestBody.Email);

            if (user == null)
            {
                return StatusCode(404, "User Not Found");
            }

            if (user.Password != requestBody.Password)
            {
                return StatusCode(400, "Password is not correct");
            }

            return StatusCode(200, new
            {
                Message = "Login Completed Successfully",
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }
    }
}
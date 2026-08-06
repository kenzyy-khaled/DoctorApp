using System;

namespace DoctorApp.Api.RequestBody
{
    public class RegisterDoctorRequestBody
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Image { get; set; }

        public DateTime DOB { get; set; }

        public int YearOfExperience { get; set; }

        public string Bio { get; set; }
    }
}
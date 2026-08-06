using System.ComponentModel.DataAnnotations;


namespace DoctorApp.Api.RequestBody
{
    public class RegisterPatientRequestBody
    {
        public string FullName { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


    }
}

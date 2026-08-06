using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DoctorApp.Entities.Users
{
    public class User
    {
        //I Want to  create Db Context File with the name of DoctorAppDbContext and add the DbSet for the Users entity in it.
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int? DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; set; }
       
        public int? PatientId { get; set; }
       
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }
    }
}    

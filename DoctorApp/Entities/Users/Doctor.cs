using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorApp.Entities.Users
{
    public class Doctor
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string Image { get; set; }

        public DateTime DOB { get; set; }

        public int YearOfExperience { get; set; }

        public string? LicenseNumber { get; set; }

        public string Bio { get; set; }

        public decimal HourRate { get; set; }


        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }
            = new List<DoctorSpecialization>();
    }
}
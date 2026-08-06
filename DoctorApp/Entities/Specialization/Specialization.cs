using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Entities
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }
            = new List<DoctorSpecialization>();
    }
}
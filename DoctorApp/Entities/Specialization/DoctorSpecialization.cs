using DoctorApp.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorApp.Entities
{
    public class DoctorSpecialization
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }

        public int SpecializationId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; } = null!;

        [ForeignKey(nameof(SpecializationId))]
        public Specialization Specialization { get; set; } = null!;
    }
}
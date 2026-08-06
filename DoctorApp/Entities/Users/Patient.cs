using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DoctorApp.Entities.Users
{
    public class Patient
    {
        //I Want to  create Db Context File with the name of DoctorAppDbContext and add the DbSet for the Patient entity in it.

        [Key]
        public int Id { get; set; }


        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

    }
}
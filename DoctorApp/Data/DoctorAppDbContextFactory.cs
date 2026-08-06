using DoctorApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoctorApp.Data
{
    public class DoctorAppDbContextFactory : IDesignTimeDbContextFactory<DoctorAppDbContext>
    {
        public DoctorAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DoctorAppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=DoctorAppDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new DoctorAppDbContext(optionsBuilder.Options);
        }
    }
}
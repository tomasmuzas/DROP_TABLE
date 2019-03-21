using BackendApartmentReservation.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<DbEmployee> Employees { get; set; }

        public DbSet<DbGroup> Groups { get; set; }

        public DbSet<DbTrip> Trips { get; set; }

        public DbSet<DbApartment> Apartments { get; set; }
    }
}

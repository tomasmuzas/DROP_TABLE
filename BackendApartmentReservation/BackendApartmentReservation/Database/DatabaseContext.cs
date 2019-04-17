namespace BackendApartmentReservation.Database
{
    using Entities;
    using Entities.Amenities;
    using Entities.Reservations;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Important entities
        public DbSet<DbEmployee> Employees { get; set; }

        public DbSet<DbGroup> Groups { get; set; }

        public DbSet<DbTrip> Trips { get; set; }

        public DbSet<DbApartment> Apartments { get; set; }

        public DbSet<DbOffice> Offices { get; set; }

        // Amenities
        public DbSet<DbEmployeeAmenitiesChecklist> Checklists { get; set; }

        public DbSet<DbCarRentAmenity> CarRentAmenities { get; set; }

        public DbSet<DbFlightAmenity> FlightAmenities { get; set; }

        public DbSet<DbLivingPlaceAmenity> LivingPlaceAmenities { get; set; }

        // Reservations
        public DbSet<DbCarReservation> CarReservations { get; set; }

        public DbSet<DbFlightReservation> FlightReservations { get; set; }

        public DbSet<DbHotelReservation> HotelReservations { get; set; }
    }
}
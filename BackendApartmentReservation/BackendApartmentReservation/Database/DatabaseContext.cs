using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Database.Entities.Reservations;

using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Important entities
        public DbSet<DbEmployee> Employees { get; set; }

        public DbSet<DbGroup> Groups { get; set; }

        public DbSet<DbEmployeeGroup> DbEmployeeGroup { get; set; }

        public DbSet<DbTrip> Trips { get; set; }

        public DbSet<DbApartment> Apartments { get; set; }

        public DbSet<DbOffice> Offices { get; set; }

        // Amenities
        public DbSet<DbEmployeeAmenitiesChecklist> Checklists { get; set; }

        public DbSet<DbCarRentAmenity> CarRentAmenities { get; set; }

        public DbSet<DbFlightAmenity> FlightAmenities { get; set; }

        public DbSet<DbLivingPlaceAmenity> LivingPlaceAmenities { get; set; }

        public DbSet<DbApartmentAmenity> ApartmentAmenities { get; set; }

        public DbSet<DbHotelAmenity> HotelAmenities { get; set; }
        // Reservations
        public DbSet<DbCarReservation> CarReservations { get; set; }

        public DbSet<DbFlightReservation> FlightReservations { get; set; }

        public DbSet<DbLivingPlaceReservation> LivingPlaceReservations { get; set; }

        public DbSet<DbHotelReservation> HotelReservations { get; set; }

        public DbSet<DbApartmentReservation> ApartmentReservations { get; set; }
    }
}

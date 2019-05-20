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

        // Employees
        public DbSet<DbEmployee> Employees { get; set; }

        //Confirmations
        public DbSet<DbConfirmation> Confirmations { get; set; }

        // Employee groups
        public DbSet<DbGroup> Groups { get; set; }

        public DbSet<DbEmployeeGroup> DbEmployeeGroup { get; set; }


        // Trips
        public DbSet<DbTrip> Trips { get; set; }

        // Cars
        public DbSet<DbCarRentAmenity> CarRentAmenities { get; set; }

        public DbSet<DbCarReservation> CarReservations { get; set; }

        // Flights
        public DbSet<DbFlightAmenity> FlightAmenities { get; set; }

        public DbSet<DbFlightReservation> FlightReservations { get; set; }

        // Offices
        public DbSet<DbOffice> Offices { get; set; }

        // Checklists
        public DbSet<DbEmployeeAmenitiesChecklist> Checklists { get; set; }

        // Living places
        public DbSet<DbLivingPlaceAmenity> LivingPlaceAmenities { get; set; }

        public DbSet<DbApartment> Apartments { get; set; }

        public DbSet<DbApartmentRoom> ApartmentRooms { get; set; }

        public DbSet<DbRoomReservation> DbRoomReservations { get; set; }

        public DbSet<DbHotelReservation> HotelReservations { get; set; }

        // Misc
        public DbSet<DbEmployeePlan> EmployeePlans { get; set; }

        public DbSet<DbAuthorization> Authorizations { get; set; }
    }
}
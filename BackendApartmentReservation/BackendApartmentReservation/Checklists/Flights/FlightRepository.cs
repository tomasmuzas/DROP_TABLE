namespace BackendApartmentReservation.Checklists.Flights
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Interfaces;

    public class FlightRepository : IFlightRepository
    {
        private readonly DatabaseContext _db;

        public FlightRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbFlightAmenity> CreateEmptyFlight()
        {
            var flightReservation = new DbFlightReservation();

            var flightAmenity = new DbFlightAmenity
            {
                BookedAt = DateTimeOffset.Now,
                FlightReservation = flightReservation
            };

            await _db.FlightReservations.AddAsync(flightReservation);
            await _db.FlightAmenities.AddAsync(flightAmenity);

            await _db.SaveChangesAsync();

            return flightAmenity;
        }

        public async Task UpdateFlight(DbFlightAmenity flight)
        {
            _db.Update(flight);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFlight(DbFlightAmenity flight)
        {
            _db.FlightAmenities.Remove(flight);
            await _db.SaveChangesAsync();

            var flightReservation = flight.FlightReservation;
            _db.FlightReservations.Remove(flightReservation);
            await _db.SaveChangesAsync();
        }
    }
}

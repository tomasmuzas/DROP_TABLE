namespace BackendApartmentReservation.Repositories.Checklist
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;

    public class FlightRepository : IFlightRepository
    {
        private readonly DatabaseContext _db;

        public FlightRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbFlightAmenity> CreateEmptyFlight()
        {
            var flightReservation = new DbFlightReservation
            {
                FlightNumber = "FL123"
            };

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
    }
}

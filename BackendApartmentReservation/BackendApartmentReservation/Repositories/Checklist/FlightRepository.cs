namespace BackendApartmentReservation.Repositories.Checklist
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Microsoft.EntityFrameworkCore;

    public class FlightRepository : IFlightRepository
    {
        private readonly DatabaseContext _db;

        public FlightRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbFlightAmenity> CreateFlightAmenityFromFlightNumber(string flightNumber)
        {
            var flightReservation = await _db.FlightReservations.SingleOrDefaultAsync(r => r.FlightNumber == flightNumber);

            if (flightReservation == default(DbFlightReservation))
            {
                flightReservation = new DbFlightReservation
                {
                    FlightNumber = flightNumber
                };

                await _db.FlightReservations.AddAsync(flightReservation);
            }

            var flightAmenity = new DbFlightAmenity
            {
                BookedAt = DateTimeOffset.Now,
                FlightReservation = flightReservation
            };

            await _db.FlightAmenities.AddAsync(flightAmenity);

            await _db.SaveChangesAsync();

            return flightAmenity;
        }
    }
}

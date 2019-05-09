using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Checklists.Flights;
    using Xunit;

    public class FlightRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateEmptyFlight_CreatesFlightWithCorrectInformation()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var repository = new FlightRepository(dbContext);

                var amenity = await repository.CreateEmptyFlight();
                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.FlightReservations.Single();
                Assert.NotNull(reservation);

                Assert.NotEqual(0, reservation.Id);
                Assert.NotEqual(0, amenity.FlightReservation.Id);

                Assert.Equal(amenity.FlightReservation.Id, reservation.Id);
            }
        }

        [Fact]
        public async Task UpdateChecklist_Success()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var oldFlight = new DbFlightAmenity
                {
                    Id = 1,
                    FlightReservation = new DbFlightReservation()
                };

                dbContext.FlightAmenities.Add(oldFlight);
                await dbContext.SaveChangesAsync();

                var repository = new FlightRepository(dbContext);

                oldFlight.FlightReservation.AirportAddress = "airportAddress";

                await repository.UpdateFlight(oldFlight);

                var flightEntry = dbContext.FlightAmenities.Single();

                Assert.Equal("airportAddress", flightEntry.FlightReservation.AirportAddress);
            }
        }

        [Fact]
        public async Task DeleteChecklist_RemovesEverything()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var flight = new DbFlightAmenity
                {
                    Id = 1,
                    FlightReservation = new DbFlightReservation()
                };

                dbContext.FlightAmenities.Add(flight);
                await dbContext.SaveChangesAsync();

                var repository = new FlightRepository(dbContext);

                await repository.DeleteFlight(flight);

                var flightEntry = dbContext.FlightAmenities.SingleOrDefault();
                var flightReservationEntry = dbContext.FlightReservations.SingleOrDefault();

                Assert.Null(flightEntry);
                Assert.Null(flightReservationEntry);
            }
        }
    }
}

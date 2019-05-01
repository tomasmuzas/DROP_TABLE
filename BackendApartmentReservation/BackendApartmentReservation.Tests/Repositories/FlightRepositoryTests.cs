namespace BackendApartmentReservation.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Repositories.Checklist;
    using Xunit;

    public class FlightRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateEmptyFlight_CreatesFlightWithCorrectInformation()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var flightNumber = "FL123";
                var repository = new FlightRepository(dbContext);

                var amenity = await repository.CreateEmptyFlight();
                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.FlightReservations.FirstOrDefault(f => f.FlightNumber == flightNumber);
                Assert.NotNull(reservation);
                Assert.Equal(reservation.FlightNumber, flightNumber);

                Assert.NotEqual(0, reservation.Id);
                Assert.NotEqual(0, amenity.FlightReservation.Id);

                Assert.Equal(amenity.FlightReservation.Id, reservation.Id);
            }
        }
    }
}

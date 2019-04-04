namespace BackendApartmentReservation.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities.Reservations;
    using Xunit;

    public class FlightRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateCarRentAmenityFromCarNumber_CreatesReservationWhenNoneExists()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var flightNumber = "FL1234";
                var repository = new FlightRepository(dbContext);

                var amenity = await repository.CreateFlightAmenityFromFlightNumber(flightNumber);
                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.FlightReservations.FirstOrDefault(f => f.FlightNumber == flightNumber);
                Assert.NotNull(reservation);
                Assert.Equal(reservation.FlightNumber, flightNumber);

                Assert.NotEqual(0, reservation.Id);
                Assert.NotEqual(0, amenity.Reservation.Id);

                Assert.Equal(amenity.Reservation.Id, reservation.Id);
            }
        }

        [Fact]
        public async Task CreateCarRentAmenityFromCarNumber_UsesExistingReservation()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var flightNumber = "FL1234";
                dbContext.FlightReservations.Add(new DbFlightReservation
                {
                    FlightNumber = flightNumber
                });
                await dbContext.SaveChangesAsync();

                var repository = new FlightRepository(dbContext);

                var amenity = await repository.CreateFlightAmenityFromFlightNumber(flightNumber);

                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.FlightReservations.Single(f => f.FlightNumber == flightNumber);
                Assert.NotEqual(0, amenity.Reservation.Id);

                Assert.Equal(amenity.Reservation.Id, reservation.Id);
            }
        }
    }
}

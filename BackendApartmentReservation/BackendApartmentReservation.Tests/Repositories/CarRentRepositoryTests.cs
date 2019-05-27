using BackendApartmentReservation.Database.Entities.Amenities;

namespace BackendApartmentReservation.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Checklists.Cars;
    using Database.Entities.Reservations;
    using Xunit;

    public class CarRentRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateEmptyCarRent_CreatesCarRentWithCorrectInformation()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var repository = new CarRentRepository(dbContext);

                var amenity = await repository.CreateEmptyCarRent();
                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.CarReservations.Single();
                Assert.NotNull(reservation);

                Assert.NotEqual(0, reservation.Id);
                Assert.NotEqual(0, amenity.CarReservation.Id);

                Assert.Equal(amenity.CarReservation.Id, reservation.Id);
            }
        }

        [Fact]
        public async Task UpdateChecklist_Success()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var oldCarRent = new DbCarRentAmenity
                {
                    Id = 1,
                    CarReservation = new DbCarReservation()
                };

                dbContext.CarRentAmenities.Add(oldCarRent);
                await dbContext.SaveChangesAsync();

                var repository = new CarRentRepository(dbContext);

                oldCarRent.CarReservation.CarNumber = "159753";

                await repository.UpdateCarRent(oldCarRent);

                var CarRentEntry = dbContext.CarRentAmenities.Single();

                Assert.Equal("159753", CarRentEntry.CarReservation.CarNumber);
            }
        }

        [Fact]
        public async Task DeleteChecklist_RemovesEverything()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var carRent = new DbCarRentAmenity
                {
                    Id = 1,
                    CarReservation = new DbCarReservation()
                };

                dbContext.CarRentAmenities.Add(carRent);
                await dbContext.SaveChangesAsync();

                var repository = new CarRentRepository(dbContext);

                await repository.DeleteCarRent(carRent);

                var carRentEntry = dbContext.CarRentAmenities.SingleOrDefault();
                var carReservationEntry = dbContext.CarReservations.SingleOrDefault();

                Assert.Null(carRentEntry);
                Assert.Null(carReservationEntry);
            }

        }
    }
}

﻿namespace BackendApartmentReservation.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities.Reservations;
    using Xunit;

    public class CarRentRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateCarRentAmenityFromCarNumber_CreatesReservationWhenNoneExists()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var carNumber = "CAR123";
                var repository = new CarRentRepository(dbContext);

                var amenity = await repository.CreateCarRentAmenityFromCarNumber(carNumber);
                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.CarReservations.FirstOrDefault(c => c.CarNumber == carNumber);
                Assert.NotNull(reservation);
                Assert.Equal(reservation.CarNumber, carNumber);

                Assert.NotEqual(0, reservation.Id);
                Assert.NotEqual(0, amenity.CarReservation.Id);

                Assert.Equal(amenity.CarReservation.Id, reservation.Id);
            }
        }

        [Fact]
        public async Task CreateCarRentAmenityFromCarNumber_UsesExistingReservation()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var carNumber = "CAR456";
                dbContext.CarReservations.Add(new DbCarReservation
                {
                    CarNumber = carNumber
                });
                await dbContext.SaveChangesAsync();

                var repository = new CarRentRepository(dbContext);

                var amenity = await repository.CreateCarRentAmenityFromCarNumber(carNumber);

                Assert.NotNull(amenity);
                Assert.True(amenity.BookedAt > new DateTimeOffset());

                var reservation = dbContext.CarReservations.Single(c => c.CarNumber == carNumber);
                Assert.NotEqual(0, amenity.CarReservation.Id);

                Assert.Equal(amenity.CarReservation.Id, reservation.Id);
            }
        }
    }
}

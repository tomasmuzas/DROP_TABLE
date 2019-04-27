namespace BackendApartmentReservation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Repositories;
    using Database.Entities;
    using Database.Entities.Amenities;
    using Xunit;

    public class TripRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task GetTrip_ReturnsTrip()
        {
            using (var context = GetNewDatabaseContext())
            {
                var trip = new DbTrip
                {
                    DepartureDate = new DateTime(2019,1,1),
                    ReturnDate = new DateTime(2019,1,1),
                    ExternalTripId = "ExternalTripId",
                    Id = 1,
                    Groups = new List<DbGroup>(),
                    DestinationOffice = new DbOffice()
                };

                context.Trips.Add(trip);
                context.SaveChanges();

                var repository = new TripRepository(context);

                var tripResult = await repository.GetTrip(trip.ExternalTripId);

                Assert.NotNull(tripResult);

                Assert.Equal(trip.Id, tripResult.Id);
                Assert.Equal(trip.ExternalTripId, tripResult.ExternalTripId);
                Assert.Equal(trip.DepartureDate, tripResult.DepartureDate);
                Assert.Equal(trip.ReturnDate, tripResult.ReturnDate);
            }
        }

        [Fact]
        public async Task GetTripChecklistsWithEmployees_ReturnsCorrectResult()
        {
            using (var context = GetNewDatabaseContext())
            {
                var checklist = new DbEmployeeAmenitiesChecklist
                {
                    Car = new DbCarRentAmenity(),
                    Employee = new DbEmployee
                    {
                        ExternalEmployeeId = "ExternalEmployeeId",
                        Email = "a@a.a",
                        FirstName = "a",
                        LastName = "b"
                    },
                    Flight = new DbFlightAmenity(),
                    Id = 1,
                    LivingPlace = new DbLivingPlaceAmenity(),
                    Trip = new DbTrip
                    {
                        ExternalTripId = "ExternalTripId"
                    }
                };

                context.Checklists.Add(checklist);
                context.SaveChanges();

                var repository = new TripRepository(context);
                var checklists = await repository.GetTripChecklistsWithEmployees("ExternalTripId");

                Assert.NotEmpty(checklists);
                foreach (var amenitiesChecklist in checklists)
                {
                    Assert.NotNull(amenitiesChecklist);
                    Assert.NotNull(amenitiesChecklist.Flight);
                    Assert.NotNull(amenitiesChecklist.Car);
                    Assert.NotNull(amenitiesChecklist.LivingPlace);
                    Assert.NotNull(amenitiesChecklist.Employee);
                    Assert.Equal("ExternalEmployeeId", amenitiesChecklist.Employee.ExternalEmployeeId);
                    Assert.Equal("a", amenitiesChecklist.Employee.FirstName);
                    Assert.Equal("b", amenitiesChecklist.Employee.LastName);
                    Assert.Equal("a@a.a", amenitiesChecklist.Employee.Email);
                }
            }
        }
    }
}

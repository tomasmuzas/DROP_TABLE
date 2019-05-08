using System;
using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Tests.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Checklists;
    using Database.Entities;
    using Xunit;

    public class ChecklistRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task AddChecklist_Success()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var repository = new ChecklistRepository(dbContext);
                var checklist = new DbEmployeeAmenitiesChecklist();

                await repository.AddChecklist(checklist);

                var checklistEntry = dbContext.Checklists.Single();

                Assert.NotEqual(0, checklistEntry.Id);
            }
        }

        [Fact]
        public async Task UpdateChecklist_Success()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var oldChecklist = new DbEmployeeAmenitiesChecklist
                {
                    Id = 1,
                    Car = new DbCarRentAmenity()
                };

                dbContext.Checklists.Add(oldChecklist);
                await dbContext.SaveChangesAsync();

                var repository = new ChecklistRepository(dbContext);

                oldChecklist.Car.BookedAt = DateTimeOffset.Now;

                await repository.UpdateChecklist(oldChecklist);

                var checklistEntry = dbContext.Checklists.Single();

                Assert.NotNull(checklistEntry.Car.BookedAt);
            }
        }

        [Fact]
        public async Task GetChecklist_FindsCorrectChecklist()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var checklist = new DbEmployeeAmenitiesChecklist
                {
                    Id = 1,
                    Employee = new DbEmployee
                    {
                        ExternalEmployeeId = "ExternalEmployeeId"
                    },
                    Trip = new DbTrip
                    {
                        ExternalTripId = "ExternalTripId"
                    }
                };

                dbContext.Checklists.Add(checklist);
                await dbContext.SaveChangesAsync();

                var repository = new ChecklistRepository(dbContext);

                var checklistResult = await repository.GetChecklist("ExternalEmployeeId", "ExternalTripId");

                Assert.NotNull(checklistResult);
                Assert.NotNull(checklistResult.Employee);
                Assert.NotNull(checklistResult.Trip);
            }
        }

        [Fact]
        public async Task GetFullChecklist_HasAllData()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var checklist = new DbEmployeeAmenitiesChecklist
                {
                    Id = 1,
                    Flight = new DbFlightAmenity
                    {
                        FlightReservation = new DbFlightReservation()
                    },
                    Car = new DbCarRentAmenity
                    {
                        CarReservation = new DbCarReservation()
                    },
                    LivingPlace = new DbLivingPlaceAmenity
                    {
                        LivingPlaceReservation = new DbLivingPlaceReservation()
                    },
                    Employee = new DbEmployee
                    {
                        ExternalEmployeeId = "ExternalEmployeeId"
                    },
                    Trip = new DbTrip
                    {
                        ExternalTripId = "ExternalTripId"
                    }
                };

                dbContext.Checklists.Add(checklist);
                await dbContext.SaveChangesAsync();

                var repository = new ChecklistRepository(dbContext);

                var checklistResult = await repository.GetFullChecklist("ExternalEmployeeId", "ExternalTripId");

                Assert.NotNull(checklistResult);
                Assert.NotNull(checklistResult.Employee);
                Assert.NotNull(checklistResult.Trip);
                Assert.NotNull(checklistResult.Car);
                Assert.NotNull(checklistResult.Car.CarReservation);
                Assert.NotNull(checklistResult.Flight);
                Assert.NotNull(checklistResult.Flight.FlightReservation);
                Assert.NotNull(checklistResult.LivingPlace);
                Assert.NotNull(checklistResult.LivingPlace.LivingPlaceReservation);
            }
        }

        [Fact]
        public async Task GetChecklistFullFlight_HasAllData()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var checklist = new DbEmployeeAmenitiesChecklist
                {
                    Id = 1,
                    Flight = new DbFlightAmenity
                    {
                        FlightReservation = new DbFlightReservation()
                    },
                    Car = new DbCarRentAmenity
                    {
                        CarReservation = new DbCarReservation()
                    },
                    LivingPlace = new DbLivingPlaceAmenity
                    {
                        LivingPlaceReservation = new DbLivingPlaceReservation()
                    },
                    Employee = new DbEmployee
                    {
                        ExternalEmployeeId = "ExternalEmployeeId"
                    },
                    Trip = new DbTrip
                    {
                        ExternalTripId = "ExternalTripId"
                    }
                };

                dbContext.Checklists.Add(checklist);
                await dbContext.SaveChangesAsync();

                var repository = new ChecklistRepository(dbContext);

                var flight = await repository.GetChecklistFullFlight("ExternalEmployeeId", "ExternalTripId");

                Assert.NotNull(flight);
                Assert.NotNull(flight.FlightReservation);
            }
        }
    }
}

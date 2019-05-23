
using BackendApartmentReservation.Checklists.Cars.Interfaces;

namespace BackendApartmentReservation.Tests.Integration
{
    using System.Threading.Tasks;
    using Checklists.Hotels.Interfaces;
    using Checklists;
    using Checklists.Flights.Interfaces;
    using Database.Entities;
    using Employees.Interfaces;
    using FakeItEasy;
    using Microsoft.Extensions.Logging.Abstractions;
    using Trips.Interfaces;
    using Xunit;
    using BackendApartmentReservation.Apartments.Interfaces;
    using BackendApartmentReservation.Groups.Interfaces;

    public class FullChecklistManagerTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateChecklistForEmployee_CreatesFullChecklist()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var employeeRepository = A.Fake<IEmployeeRepository>(o => o.Strict());
                var tripRepository = A.Fake<ITripRepository>(o => o.Strict());
                var groupRepository = A.Fake<IGroupRepository>(o => o.Strict());
                var flightRepository = A.Fake<IFlightRepository>(o => o.Strict());
                var carRentRepository = A.Fake<ICarRentRepository>(o => o.Strict());
                var livingPlaceRepository = A.Fake<ILivingPlaceRepository>(o => o.Strict());
                var hotelRepository = A.Fake <IHotelRepository>(o => o.Strict());
                var apartmentRepository = A.Fake<IApartmentRepository>(o => o.Strict());
                var checklistRepository = new ChecklistRepository(dbContext);

                var checklistManager = new ChecklistManager(
                    employeeRepository,
                    checklistRepository,
                    tripRepository,
                    groupRepository,
                    flightRepository,
                    carRentRepository,
                    livingPlaceRepository,
                    hotelRepository,
                    apartmentRepository,
                    new NullLogger<ChecklistManager>());

                var employee = new DbEmployee
                {
                    Id = 5,
                    ExternalEmployeeId = "ExternalEmployeeId",
                    Email = "test@test.t",
                    FirstName = "Test",
                    LastName = "Testsen",
                    Office = new DbOffice()
                };

                var trip = new DbTrip
                {
                    Id = 5,
                    ExternalTripId = "ExternalTripId"
                };

                dbContext.Employees.Add(employee);
                dbContext.Trips.Add(trip);
                await dbContext.SaveChangesAsync();

                A.CallTo(() => employeeRepository.GetEmployeeByEmployeeId(employee.ExternalEmployeeId))
                    .Returns(Task.FromResult(employee));

                A.CallTo(() => tripRepository.GetTrip(trip.ExternalTripId))
                    .Returns(Task.FromResult(trip));

                var checklist = await checklistManager.CreateEmptyChecklistForEmployee(
                    employee.ExternalEmployeeId,
                    trip.ExternalTripId);

                Assert.NotNull(checklist);

                Assert.NotNull(checklist.Employee);
                Assert.Equal(employee.Id, checklist.Employee.Id);
                Assert.NotNull(checklist.Trip);
                Assert.Equal(trip.Id, checklist.Trip.Id);
            }
        }
    }
}

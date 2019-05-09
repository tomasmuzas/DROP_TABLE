
namespace BackendApartmentReservation.Tests.Integration
{
    using System.Threading.Tasks;
    using Checklists;
    using Checklists.Flights.Interfaces;
    using Database.Entities;
    using Employees.Interfaces;
    using FakeItEasy;
    using Microsoft.Extensions.Logging.Abstractions;
    using Trips.Interfaces;
    using Xunit;

    public class FullChecklistManagerTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateChecklistForEmployee_CreatesFullChecklist()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var employeeRepository = A.Fake<IEmployeeRepository>(o => o.Strict());
                var tripRepository = A.Fake<ITripRepository>(o => o.Strict());
                var fligtRepository = A.Fake<IFlightRepository>(o => o.Strict());
                var checklistRepository = new ChecklistRepository(dbContext);

                var checklistManager = new ChecklistManager(
                    employeeRepository,
                    checklistRepository,
                    tripRepository,
                    fligtRepository,
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

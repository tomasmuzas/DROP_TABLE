namespace BackendApartmentReservation.Tests.Integration
{
    using System.Threading.Tasks;
    using BackendApartmentReservation.Managers;
    using BackendApartmentReservation.Repositories;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities;
    using FakeItEasy;
    using Microsoft.Extensions.Logging.Abstractions;
    using Xunit;

    public class FullChecklistManagerTests : DatabaseTestBase
    {
        [Fact]
        public async Task CreateChecklistForEmployee_CreatesFullChecklist()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var employeeRepository = A.Fake<IEmployeeRepository>(o => o.Strict());
                var flightRepository = new FlightRepository(dbContext);
                var carRentRepository = new CarRentRepository(dbContext);
                var checklistRepository = new ChecklistRepository(dbContext);

                var checklistManager = new ChecklistManager(
                    employeeRepository,
                    flightRepository,
                    carRentRepository,
                    checklistRepository,
                    new NullLogger<ChecklistManager>());
                var employee = new DbEmployee
                {
                    Id = 5,
                    Email = "test@test.t",
                    FirstName = "Test",
                    LastName = "Testsen",
                    Office = new DbOffice()
                };
                dbContext.Employees.Add(employee);
                await dbContext.SaveChangesAsync();

                A.CallTo(() => employeeRepository.GetEmployeeById(employee.Id))
                    .Returns(Task.FromResult(employee));

                var flightInfo = new FlightReservationInfo
                {
                    Required = true,
                    FlightNumber = "FL1234"
                };

                var carInfo = new CarReservationInfo
                {
                    Required = true,
                    CarNumber = "CAR123"
                };

                var checklist = await checklistManager.CreateChecklistForEmployee(
                    employee.Id,
                    flightInfo,
                    carInfo);

                Assert.NotNull(checklist);

                Assert.NotNull(checklist.Employee);
                Assert.Equal(employee.Id, checklist.Employee.Id);

                Assert.NotNull(checklist.Flight);
                Assert.NotEqual(0, checklist.Flight.Id);
                Assert.NotNull(checklist.Flight.Reservation);
                Assert.NotEqual(0, checklist.Flight.Reservation.Id);
                Assert.Equal(flightInfo.FlightNumber, checklist.Flight.Reservation.FlightNumber);

                Assert.NotNull(checklist.Car);
                Assert.NotEqual(0, checklist.Car.Id);
                Assert.NotNull(checklist.Car.Car);
                Assert.NotEqual(0, checklist.Car.Car.Id);
                Assert.Equal(carInfo.CarNumber, checklist.Car.Car.CardPlateNumber);
            }
        }
    }
}

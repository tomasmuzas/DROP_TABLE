using BackendHotelReservation.Repositories.Checklist;

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
                var apartmentRepository = new ApartmentRepository(dbContext);
                var hotelRepository = new HotelRepository(dbContext);
                var livingSpaceRepository = new LivingPlaceRepository(dbContext, apartmentRepository, hotelRepository);
                var checklistRepository = new ChecklistRepository(dbContext);

                var checklistManager = new ChecklistManager(
                    employeeRepository,
                    flightRepository,
                    carRentRepository,
                    livingSpaceRepository,
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

                var livingSpaceInfo = new LivingPlaceReservationInfo
                {
                    Required = true,
                    ApartmentReservationInfo = new ApartmentReservationInfo {Required = true, ApartmentAddress = "Vilnius"},
                    HotelReservationInfo = new HotelReservationInfo { Required = false, HotelAddress = "Kaunas"}
                };


            var checklist = await checklistManager.CreateChecklistForEmployee(
                    employee.Id,
                    flightInfo,
                    carInfo,
                    livingSpaceInfo
                    );

                Assert.NotNull(checklist);

                Assert.NotNull(checklist.Employee);
                Assert.Equal(employee.Id, checklist.Employee.Id);

                Assert.NotNull(checklist.Flight);
                Assert.NotEqual(0, checklist.Flight.Id);
                Assert.NotNull(checklist.Flight.FlightReservation);
                Assert.NotEqual(0, checklist.Flight.FlightReservation.Id);
                Assert.Equal(flightInfo.FlightNumber, checklist.Flight.FlightReservation.FlightNumber);

                Assert.NotNull(checklist.Car);
                Assert.NotEqual(0, checklist.Car.Id);
                Assert.NotNull(checklist.Car.CarReservation);
                Assert.NotEqual(0, checklist.Car.CarReservation.Id);
                Assert.Equal(carInfo.CarNumber, checklist.Car.CarReservation.CarNumber);

                Assert.NotNull(checklist.LivingPlace);
                Assert.NotEqual(0, checklist.LivingPlace.Id);
                Assert.NotNull(checklist.LivingPlace.LivingPlaceReservation.ApartmentReservation);
                Assert.NotEqual(0, checklist.LivingPlace.LivingPlaceReservation.Id);
                Assert.Equal(livingSpaceInfo.ApartmentReservationInfo.ApartmentAddress, checklist.LivingPlace.LivingPlaceReservation.ApartmentReservation.Address);
                Assert.Null(checklist.LivingPlace.LivingPlaceReservation.HotelReservation);

            }
        }
    }
}

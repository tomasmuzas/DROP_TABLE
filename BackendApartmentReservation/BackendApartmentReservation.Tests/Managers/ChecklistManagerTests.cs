namespace BackendApartmentReservation.Tests.Managers
{
    using System.Threading.Tasks;
    using BackendApartmentReservation.Managers;
    using BackendApartmentReservation.Repositories;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities;
    using Database.Entities.Amenities;
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Xunit;

    public class ChecklistManagerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;

        private readonly IChecklistRepository _cheklistRepository;

        private readonly ILogger<ChecklistManager> _logger;

        private readonly ChecklistManager _manager;

        public ChecklistManagerTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _flightRepository = A.Fake<IFlightRepository>();
            _carRentRepository = A.Fake<ICarRentRepository>();

            _cheklistRepository = A.Fake<IChecklistRepository>();

            _logger = new NullLogger<ChecklistManager>();

            _manager = new ChecklistManager(
                _employeeRepository,
                _flightRepository,
                _carRentRepository,
                _cheklistRepository,
                _logger);
        }

        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [Theory]
        public async Task CreateChecklistForEmployee_CreatesProperChecklist(bool flight, bool car)
        {
            var employee = new DbEmployee
            {
                Id = 5
            };

            var employeeRepositoryCall = A.CallTo(() => _employeeRepository.GetEmployeeById(employee.Id));

            var checklistRepositoryCall =
                A.CallTo(() => _cheklistRepository.AddChecklist(A<DbEmployeeAmenitiesChecklist>._));

            var flightNumber = "FL1234";
            var flightRepositoryCall =
                A.CallTo(() => _flightRepository.CreateFlightAmenityFromFlightNumber(flightNumber));

            var carNumber = "CAR123";
            var carRepositoryCall = A.CallTo(() => _carRentRepository.CreateCarRentAmenityFromCarNumber(carNumber));

            if (flight)
            {
                flightRepositoryCall.Returns(new DbFlightAmenity());
            }

            if (car)
            {
                carRepositoryCall.Returns(new DbCarRentAmenity());
            }

            employeeRepositoryCall.Returns(employee);
            checklistRepositoryCall.Returns(Task.CompletedTask);

            // TODO: @tomu figure out logging tests
            //var carLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created car rent amenity for employee {employee.Id}."));

            //var flightLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created flight amenity for employee {employee.Id}."));

            var checklist = await _manager.CreateChecklistForEmployee(
                employee.Id,
                new FlightReservationInfo
                {
                    Required = flight,
                    FlightNumber = flightNumber
                },
                new CarReservationInfo
                {
                    Required = car,
                    CarNumber = carNumber
                });

            employeeRepositoryCall.MustHaveHappenedOnceExactly();
            checklistRepositoryCall.MustHaveHappenedOnceExactly();

            Assert.Equal(checklist.Employee, employee);

            if (car)
            {
                carRepositoryCall.MustHaveHappenedOnceExactly();
                //carLogCall.MustHaveHappened();
            }
            else
            {
                carRepositoryCall.MustNotHaveHappened();
                //carLogCall.MustNotHaveHappened();
            }

            if (flight)
            {
                flightRepositoryCall.MustHaveHappenedOnceExactly();
                //flightLogCall.MustHaveHappened();
            }
            else
            {
                flightRepositoryCall.MustNotHaveHappened();
                //flightLogCall.MustNotHaveHappened();
            }
        }
    }
}

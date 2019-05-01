namespace BackendApartmentReservation.Tests.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Managers;
    using BackendApartmentReservation.Repositories;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities;
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Xunit;

    public class ChecklistManagerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IFlightRepository _flightRepository;

        private readonly ChecklistManager _manager;

        public ChecklistManagerTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _tripRepository = A.Fake<ITripRepository>();
            _flightRepository = A.Fake<IFlightRepository>();

            _checklistRepository = A.Fake<IChecklistRepository>(o => o.Strict());

            ILogger<ChecklistManager> logger = new NullLogger<ChecklistManager>();

            _manager = new ChecklistManager(
                _employeeRepository,
                _checklistRepository,
                _tripRepository,
                _flightRepository,
                logger);
        }

        [Fact]
        public async Task CreateChecklistForEmployee_CreatesProperChecklist()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var employeeRepositoryCall = A.CallTo(() => _employeeRepository.GetEmployeeByEmployeeId(employee.ExternalEmployeeId));

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var tripRepositoryCall = A.CallTo(() => _tripRepository.GetTrip(trip.ExternalTripId));

            var checklistRepositoryCall =
                A.CallTo(() => _checklistRepository.AddChecklist(A<DbEmployeeAmenitiesChecklist>._));

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult((DbEmployeeAmenitiesChecklist)null));

            employeeRepositoryCall.Returns(employee);
            tripRepositoryCall.Returns(trip);
            checklistRepositoryCall.Returns(Task.CompletedTask);

            // TODO: @tomu figure out logging tests
            //var carLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created car rent amenity for employee {employee.Id}."));

            //var flightLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created flight amenity for employee {employee.Id}."));

            var checklist = await _manager.CreateEmptyChecklistForEmployee(
                employee.ExternalEmployeeId,
                trip.ExternalTripId);

            employeeRepositoryCall.MustHaveHappenedOnceExactly();
            tripRepositoryCall.MustHaveHappenedOnceExactly();
            checklistRepositoryCall.MustHaveHappenedOnceExactly();
            getChecklistCall.MustHaveHappenedOnceExactly();

            Assert.Equal(checklist.Employee, employee);
            Assert.Equal(checklist.Trip, trip);
        }
    }
}

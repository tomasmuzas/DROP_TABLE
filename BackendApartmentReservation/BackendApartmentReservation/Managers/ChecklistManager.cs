namespace BackendApartmentReservation.Managers
{
    using System.Threading.Tasks;
    using Database.Entities;
    using Microsoft.Extensions.Logging;
    using Repositories;
    using Repositories.Checklist;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;

        private readonly IChecklistRepository _cheklistRepository;

        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IFlightRepository flightRepository,
            ICarRentRepository carRentRepository,
            IChecklistRepository checklistRepository,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _flightRepository = flightRepository;
            _carRentRepository = carRentRepository;

            _cheklistRepository = checklistRepository;

            _logger = logger;
        }

        public async Task<DbEmployeeAmenitiesChecklist> CreateChecklistForEmployee(
            int employeeId,
            FlightReservationInfo flightReservationInfo,
            CarReservationInfo carReservationInfo)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId);

            var checklist = new DbEmployeeAmenitiesChecklist
            {
                Employee = employee
            };

            if (flightReservationInfo.Required)
            {
                var flightAmenity =
                    await _flightRepository.CreateFlightAmenityFromFlightNumber(flightReservationInfo.FlightNumber);
                checklist.Flight = flightAmenity;
                _logger.LogInformation($"Created flight amenity for employee {employee.Id}.");
            }

            if (carReservationInfo.Required)
            {
                var carAmenity =
                    await _carRentRepository.CreateCarRentAmenityFromCarNumber(carReservationInfo.CarNumber);
                checklist.Car = carAmenity;
                _logger.LogInformation($"Created car rent amenity for employee {employee.Id}.");
            }

            await _cheklistRepository.AddChecklist(checklist);

            return checklist;
        }
    }
}
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    using System;
    using Microsoft.Extensions.Logging;
    using Repositories.Checklist;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;

        private readonly IFlightManager _flightManager;

        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            ITripRepository tripRepository,
            IFlightManager flightManager,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _tripRepository = tripRepository;
            _checklistRepository = checklistRepository;

            _flightManager = flightManager;

            _logger = logger;
        }

        public async Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(
            string employeeId,
            string tripId)
        {
            var existingChecklist = await _checklistRepository.GetChecklist(employeeId, tripId);

            if (existingChecklist != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already exists.");
                throw new ArgumentException("");
            }

            var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeId);
            var trip = await _tripRepository.GetTrip(tripId);

            var checklist = new DbEmployeeAmenitiesChecklist
            {
                Employee = employee,
                Trip = trip
            };

            await _checklistRepository.AddChecklist(checklist);
            _logger.LogInformation($"Successfully created empty checklist for trip {tripId}, employee {employeeId}");

            return checklist;
        }

        public async Task AddFlightForEmployee(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            if (checklist.Flight != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already has a flight.");
                throw new ArgumentException("");
            }

            await _flightManager.AddEmptyFlightToChecklist(checklist);
            _logger.LogInformation($"Added empty flight to the checklist for employee {employeeId} and trip {tripId}");
        }
    }
}

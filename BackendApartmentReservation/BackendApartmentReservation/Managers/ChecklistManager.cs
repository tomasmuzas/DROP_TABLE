using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    using Microsoft.Extensions.Logging;
    using Repositories.Checklist;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;

        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            ITripRepository tripRepository,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _tripRepository = tripRepository;
            _checklistRepository = checklistRepository;

            _logger = logger;
        }

        public async Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(
            string employeeId,
            string tripId)
        {
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
    }
}

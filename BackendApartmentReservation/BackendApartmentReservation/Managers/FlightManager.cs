namespace BackendApartmentReservation.Managers
{
    using System.Threading.Tasks;
    using Database.Entities;
    using Repositories.Checklist;

    public class FlightManager : IFlightManager
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IChecklistRepository _checklistRepository;

        public FlightManager(
            IFlightRepository flightRepository,
            IChecklistRepository checklistRepository)
        {
            _flightRepository = flightRepository;
            _checklistRepository = checklistRepository;
        }

        public async Task AddEmptyFlightToChecklist(DbEmployeeAmenitiesChecklist checklist)
        {
            var flight = await _flightRepository.CreateEmptyFlight();
            checklist.Flight = flight;
            await _checklistRepository.UpdateChecklist(checklist);
        }
    }
}

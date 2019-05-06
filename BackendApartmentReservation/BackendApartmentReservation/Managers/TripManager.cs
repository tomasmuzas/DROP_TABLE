using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Repositories;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public class TripManager : ITripManager
    {
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistManager _checklistManager;

        public TripManager(ITripRepository tripRepository, IChecklistManager checklistManager)

        {
            _tripRepository = tripRepository;
            _checklistManager = checklistManager;
        }

        public async Task<string> CreateBasicTrip(CreateTripRequest tripRequest)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest);

            foreach (var group in trip.Groups)
            {
                group.Employees.ForEach(async e =>
                {
                    await _checklistManager.CreateEmptyChecklistForEmployee(e.ExternalEmployeeId, trip.ExternalTripId);
                });
            }

            return trip.ExternalTripId;
        }
    }
}

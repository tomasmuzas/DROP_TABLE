using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Repositories;
using BackendApartmentReservation.Repositories.Checklist;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public class TripManager : ITripManager
    {
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;

        public TripManager(ITripRepository tripRepository, IChecklistRepository checklistRepository)
        {
            _tripRepository = tripRepository;
            _checklistRepository = checklistRepository;
        }

        public async Task<string> CreateBasicTrip(CreateTripRequest tripRequest)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest);

            foreach (var group in trip.Groups)
            {
                group.Employees.ForEach(async e =>
                {
                    var checklist = new DbEmployeeAmenitiesChecklist
                    {
                        Employee = e,
                        Trip = trip
                    };
                    await _checklistRepository.AddChecklist(checklist);
                });
            }

            return trip.ExternalTripId;
        }
    }
}

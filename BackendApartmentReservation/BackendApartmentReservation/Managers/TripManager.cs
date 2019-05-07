using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Repositories;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public class TripManager : ITripManager
    {
        private readonly ITripRepository _tripRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IChecklistManager _checklistManager;

        public TripManager(ITripRepository tripRepository, IGroupRepository groupRepository, IChecklistManager checklistManager)
        {
            _tripRepository = tripRepository;
            _groupRepository = groupRepository;
            _checklistManager = checklistManager;
        }

        public async Task<string> CreateBasicTrip(CreateTripRequest tripRequest)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest);

            foreach (var group in trip.Groups)
            {
                var employeesGroup = await _groupRepository.GetEmployeeGroupById(group.ExternalGroupId);
                foreach (var employeeGroup in employeesGroup)
                {
                    await _checklistManager.CreateEmptyChecklistForEmployee(employeeGroup.DbEmployee.ExternalEmployeeId, trip.ExternalTripId);
                }
            }

            return trip.ExternalTripId;
        }
    }
}

namespace BackendApartmentReservation.Trips
{
    using System.Threading.Tasks;
    using Checklists.Interfaces;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Groups.Interfaces;
    using Interfaces;

    public class TripManager : ITripManager
    {
        private readonly ITripRepository _tripRepository;
        private readonly IGroupManager _groupManager;
        private readonly IChecklistManager _checklistManager;

        public TripManager(
            ITripRepository tripRepository,
            IGroupManager groupManager,
            IChecklistManager checklistManager)
        {
            _tripRepository = tripRepository;
            _groupManager = groupManager;
            _checklistManager = checklistManager;
        }

        public async Task<TripCreatedResponse> CreateBasicTrip(CreateTripRequest tripRequest)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest);

            foreach (var group in trip.Groups)
            {
                var employeesGroup = await _groupManager.GetEmployeeGroupsByGroupId(group.ExternalGroupId);
                foreach (var employeeGroup in employeesGroup)
                {
                    await _checklistManager.CreateEmptyChecklistForEmployee(employeeGroup.DbEmployee.ExternalEmployeeId,
                        trip.ExternalTripId);
                }
            }

            return new TripCreatedResponse
            {
                TripId = trip.ExternalTripId
            };
        }
    }
}
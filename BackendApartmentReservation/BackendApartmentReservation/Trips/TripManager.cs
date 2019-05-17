using System;
using System.Collections.Generic;
using System.Linq;

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

        public async Task<TripCreatedResponse> CreateBasicTrip(CreateTripRequest tripRequest, string managerId)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest, managerId);

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

        public async Task<bool> IsPossibleToMergeTrips(string firstTripId, string secondTripId)
        {
            var firstTrip = await _tripRepository.GetTrip(firstTripId);
            var secondTrip = await _tripRepository.GetTrip(secondTripId);

            var timeSpanDays = (firstTrip.DepartureDate - secondTrip.DepartureDate).TotalDays;
            return timeSpanDays <= 1 && firstTrip.DestinationOffice==secondTrip.DestinationOffice;
        }

        public async Task<List<string>> GetAllMergeableTrips(string tripId)
        {
            var trip = await _tripRepository.GetTrip(tripId);

            var employeeId = trip.TripCreator.ExternalEmployeeId;

            var allTrips = await _tripRepository.GetAllTrips(employeeId);

            var mergeableTrips = allTrips.Where(t => IsPossibleToMergeTrips(tripId, t.ExternalTripId).Result).ToList();

            var mergeableTripsIds = new List<string>();
            foreach (var mergeableTrip in mergeableTrips)
            {
                mergeableTripsIds.Add(mergeableTrip.ExternalTripId);
            }

            return mergeableTripsIds;


        }
    }
}
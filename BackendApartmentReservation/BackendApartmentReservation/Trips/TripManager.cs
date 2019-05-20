using System;
using System.Collections.Generic;
using System.Linq;
using BackendApartmentReservation.Infrastructure.Exceptions;

namespace BackendApartmentReservation.Trips
{
    using System.Threading.Tasks;
    using BackendApartmentReservation.Database.Entities;
    using BackendApartmentReservation.Employees.Interfaces;
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
        private readonly IEmployeePlanRepository _employeePlanRepository;

        public TripManager(
            ITripRepository tripRepository,
            IGroupManager groupManager,
            IChecklistManager checklistManager,
            IEmployeePlanRepository employeePlanRepository)
        {
            _tripRepository = tripRepository;
            _groupManager = groupManager;
            _checklistManager = checklistManager;
            _employeePlanRepository = employeePlanRepository;
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

                    var employeePlan = new DbEmployeePlan();
                    employeePlan.StartDate = trip.DepartureDate;
                    employeePlan.EndDate = trip.ReturnDate;
                    employeePlan.Employee = employeeGroup.DbEmployee;
                    await _employeePlanRepository.CreateEmployeePlan(employeePlan);
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
            if (firstTrip == null || secondTrip == null)
            {
                throw new ErrorCodeException(ErrorCodes.TripNotFound);
            }

            var timeSpanDays = (firstTrip.DepartureDate - secondTrip.DepartureDate).TotalDays;
            return timeSpanDays <= 1 && firstTrip.DestinationOffice.ExternalOfficeId.Equals(secondTrip.DestinationOffice.ExternalOfficeId);
        }

        public async Task<IEnumerable<string>> GetAllMergeableTrips(string tripId)
        {

            var trip = await _tripRepository.GetTrip(tripId);

            if (trip == null)
            {
                throw new ErrorCodeException(ErrorCodes.TripNotFound);
            }

            var allTrips = await _tripRepository.GetAllTrips();

            var mergeableTripsIds = allTrips.Where(t => IsPossibleToMergeTrips(tripId, t.ExternalTripId).Result).Select(t => t.ExternalTripId);

            return mergeableTripsIds;
        }
    }
}
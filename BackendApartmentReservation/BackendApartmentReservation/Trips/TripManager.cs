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
        private readonly IEmployeeRepository _employeeRepository;

        public TripManager(
            ITripRepository tripRepository,
            IGroupManager groupManager,
            IChecklistManager checklistManager,
            IEmployeeRepository employeeRepository)
        {
            _tripRepository = tripRepository;
            _groupManager = groupManager;
            _checklistManager = checklistManager;
            _employeeRepository = employeeRepository;
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
                    await _employeeRepository.CreateEmployeePlan(employeePlan);
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

        public async Task<TripCreatedResponse> MergeTrips(MergeTripsRequest mergeTripsRequest, string managerId)
        {
            var isMergeable = await IsPossibleToMergeTrips(mergeTripsRequest.FirstTripId, mergeTripsRequest.SecondTripId);
            if (!isMergeable)
            {
                throw new ErrorCodeException(ErrorCodes.TripsNotMergeable);
            }

            var firstTrip = await _tripRepository.GetTrip(mergeTripsRequest.FirstTripId);
            var secondTrip = await _tripRepository.GetTrip(mergeTripsRequest.SecondTripId);

            var departureDate = firstTrip.DepartureDate <= secondTrip.DepartureDate ? firstTrip.DepartureDate : secondTrip.DepartureDate;
            var returnDate = firstTrip.ReturnDate >= secondTrip.ReturnDate ? firstTrip.ReturnDate : secondTrip.ReturnDate;

            var employeeIds = new List<string>();
            var groups = firstTrip.Groups;
            groups.AddRange(secondTrip.Groups);

            foreach (var group in groups)
            {
                var employeeGroups = (await _groupManager.GetEmployeeGroupsByGroupId(group.ExternalGroupId)).ToList();
                employeeGroups.ForEach(eg => employeeIds.Add(eg.DbEmployee.ExternalEmployeeId));
            }

            var tripRequest = new CreateTripRequest
            {
                DepartureDate = departureDate,
                DestinationOfficeId = firstTrip.DestinationOffice.ExternalOfficeId,
                EmployeeIds = employeeIds,
                ReturnDate = returnDate
            };

            var mergedTrip = await _tripRepository.CreateTrip(tripRequest, managerId);

            return new TripCreatedResponse
            {
                TripId = mergedTrip.ExternalTripId
            };
        }
    }
}
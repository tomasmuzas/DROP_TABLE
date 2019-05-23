using BackendApartmentReservation.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BackendApartmentReservation.Trips
{
    using BackendApartmentReservation.Database.Entities;
    using BackendApartmentReservation.Employees.Interfaces;
    using Checklists.Interfaces;
    using Confirmations.Interfaces;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Groups.Interfaces;
    using Interfaces;
    using System.Threading.Tasks;

    public class TripManager : ITripManager
    {
        private readonly ITripRepository _tripRepository;
        private readonly IGroupManager _groupManager;
        private readonly IChecklistManager _checklistManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfirmationRepository _confirmationRepository;

        public TripManager(
            ITripRepository tripRepository,
            IGroupManager groupManager,
            IChecklistManager checklistManager,
            IEmployeeRepository employeeRepository,
            IConfirmationRepository confirmationRepository)
        {
            _tripRepository = tripRepository;
            _groupManager = groupManager;
            _checklistManager = checklistManager;
            _employeeRepository = employeeRepository;
            _confirmationRepository = confirmationRepository;
        }

        public async Task<TripCreatedResponse> CreateBasicTrip(CreateTripRequest tripRequest, string managerId)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest, managerId);

            foreach (var group in trip.Groups)
            {
                var employeesGroup = await _groupManager.GetEmployeeGroupsByGroupId(group.ExternalGroupId);
                foreach (var employeeGroup in employeesGroup)
                {
                    var checklist = await _checklistManager.CreateEmptyChecklistForEmployee(employeeGroup.DbEmployee.ExternalEmployeeId,
                        trip.ExternalTripId);

                    await _confirmationRepository.CreateConfirmation(
                        employeeGroup.DbEmployee,
                        trip,
                        ConfirmationType.TripParticipation);

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

        public bool IsPossibleToMergeTrips(DbTrip firstTrip, DbTrip secondTrip)
        {
            if (firstTrip == null || secondTrip == null)
            {
                throw new ErrorCodeException(ErrorCodes.TripNotFound);
            }

            var timeSpanDays = (firstTrip.DepartureDate - secondTrip.DepartureDate).TotalDays;
            return System.Math.Abs(timeSpanDays) <= 1 && firstTrip.DestinationOffice.ExternalOfficeId.Equals(secondTrip.DestinationOffice.ExternalOfficeId);
        }

        public async Task<bool> IsPossibleToMergeTrips(string firstTripId, string secondTripId)
        {
            var firstTrip = await _tripRepository.GetTrip(firstTripId);
            var secondTrip = await _tripRepository.GetTrip(secondTripId);
            return IsPossibleToMergeTrips(firstTrip, secondTrip);
        }

        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllMergeableTrips(string tripId)
        {

            var trip = await _tripRepository.GetTrip(tripId);

            if (trip == null)
            {
                throw new ErrorCodeException(ErrorCodes.TripNotFound);
            }

            var allTrips = await _tripRepository.GetAllTrips();

            var basicTripInformationResponses = allTrips.Where(t => IsPossibleToMergeTrips(trip, t) && t.ExternalTripId != tripId)
                .Select(t => new BasicTripInformationResponse
                {
                    TripId = t.ExternalTripId,
                    StartTime = t.DepartureDate,
                    EndTime = t.ReturnDate,
                    Office = new OfficeInfoResponse
                    {
                        Id = t.DestinationOffice.ExternalOfficeId,
                        Address = t.DestinationOffice.Address
                    }
                });

            return basicTripInformationResponses;
        }

        public async Task<TripCreatedResponse> MergeTrips(MergeTripsRequest mergeTripsRequest, string managerId)
        {
            var firstTripToMerge = await _tripRepository.GetTrip(mergeTripsRequest.FirstTripId);
            var secondTripToMerge = await _tripRepository.GetTrip(mergeTripsRequest.SecondTripId);
            var isMergeable = IsPossibleToMergeTrips(firstTripToMerge, secondTripToMerge);
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

            var checklists = (await _checklistManager.GetAllTripChecklists(mergeTripsRequest.FirstTripId)).ToList();
            checklists.AddRange((await _checklistManager.GetAllTripChecklists(mergeTripsRequest.SecondTripId)).ToList());

            foreach (var checklist in checklists)
            {
                await _checklistManager.UpdateChecklistTrip(checklist, mergedTrip.ExternalTripId);
            }

            await _tripRepository.DeleteTrip(mergeTripsRequest.FirstTripId);
            await _tripRepository.DeleteTrip(mergeTripsRequest.SecondTripId);

            return new TripCreatedResponse
            {
                TripId = mergedTrip.ExternalTripId
            };
        }
    }
}
namespace BackendApartmentReservation.Trips
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confirmations;
    using Confirmations.Interfaces;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Responses;
    using Interfaces;
    using LivingPlace.Interfaces;

    public class TripInformationManager : ITripInformationManager
    {
        private readonly ITripRepository _tripRepository;
        private readonly IConfirmationRepository _confirmationRepository;
        private readonly ILivingPlaceManager _livingPlaceManager;

        public TripInformationManager(
            ITripRepository tripRepository,
            IConfirmationRepository confirmationRepository,
            ILivingPlaceManager livingPlaceManager)
        {
            _tripRepository = tripRepository;
            _confirmationRepository = confirmationRepository;
            _livingPlaceManager = livingPlaceManager;
        }

        public async Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId)
        {
            var trip = await _tripRepository.GetTrip(tripId);

            var checklists = await _tripRepository.GetTripChecklistsWithEmployees(tripId);

            var checklistInformations = checklists
                .Select(c => new BasicPersonalChecklistInfo
                {
                    Employee = new EmployeeInfo
                    {
                        Id = c.Employee.ExternalEmployeeId,
                        Email = c.Employee.Email,
                        FirstName = c.Employee.FirstName,
                        LastName = c.Employee.LastName
                    },
                    HasAcceptedTripConfirmation = _confirmationRepository.HasAcceptedTripParticipation(c.Employee.ExternalEmployeeId, tripId),
                    IsApartmentRequired = c.LivingPlace != null,
                    IsFlightRequired = c.Flight != null,
                    IsCarRentRequired = c.Car != null
                })
                .ToList();

            var availableRooms = await
                _livingPlaceManager.GetNumberOfAvailableApartmentRooms(tripId, trip.DepartureDate, trip.ReturnDate);

            return new BasicTripInformationResponse
            {
                TripId = tripId,
                StartTime = trip.DepartureDate,
                EndTime = trip.ReturnDate,
                AvailableApartments = availableRooms,
                ChecklistInfos = checklistInformations
            };
        }

        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllOrganizedTripsInformation(string employeeId)
        {
            var trips = await _tripRepository.GetAllOrganizedTripsOfEmployee(employeeId);
            return trips.Select(t => new BasicTripInformationResponse
            {
                StartTime = t.DepartureDate,
                EndTime = t.ReturnDate,
                TripId = t.ExternalTripId
            });
        }

        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllParticipatingTripsInformation(string employeeId)
        {
            var trips = await _tripRepository.GetAllParticipatingTripsOfEmployee(employeeId);
            return trips.Select(t => new BasicTripInformationResponse
            {
                StartTime = t.DepartureDate,
                EndTime = t.ReturnDate,
                TripId = t.ExternalTripId
            });
        }
    }
}
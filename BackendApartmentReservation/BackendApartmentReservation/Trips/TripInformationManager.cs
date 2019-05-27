namespace BackendApartmentReservation.Trips
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confirmations;
    using Confirmations.Interfaces;
    using Database.Entities;
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

        public async Task<BasicTripInformationResponse> GetBasicTripInformation(DbTrip trip)
        {
            var checklists = await _tripRepository.GetTripChecklistsWithEmployees(trip.ExternalTripId);

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
                    HasAcceptedTripConfirmation = _confirmationRepository.HasAcceptedTripParticipation(c.Employee.ExternalEmployeeId, trip.ExternalTripId),
                    IsApartmentRequired = c.LivingPlace != null,
                    IsFlightRequired = c.Flight != null,
                    IsCarRentRequired = c.Car != null
                })
                .ToList();

            var availableRooms = await
                _livingPlaceManager.GetNumberOfAvailableApartmentRooms(trip.ExternalTripId, trip.DepartureDate, trip.ReturnDate);

            return new BasicTripInformationResponse
            {
                TripId = trip.ExternalTripId,
                StartTime = trip.DepartureDate,
                EndTime = trip.ReturnDate,
                AvailableApartments = availableRooms,
                Office = new OfficeInfoResponse
                {
                    Address = trip.DestinationOffice.Address
                },
                ChecklistInfos = checklistInformations
            };
        }

        public async Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId)
        {
            var trip = await _tripRepository.GetTrip(tripId);
            return await GetBasicTripInformation(trip);
        }

        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllOrganizedTripsInformation(string employeeId)
        {
            var trips = await _tripRepository.GetAllOrganizedTripsOfEmployee(employeeId);
            return trips.Select(t => new BasicTripInformationResponse
            {
                StartTime = t.DepartureDate,
                EndTime = t.ReturnDate,
                TripId = t.ExternalTripId,
                Office = new OfficeInfoResponse
                {
                    Address = t.DestinationOffice.Address
                }
            });
        }

        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllParticipatingTripsInformation(string employeeId)
        {
            var trips = await _tripRepository.GetAllParticipatingTripsOfEmployee(employeeId);

            var infos = new List<BasicTripInformationResponse>();
            foreach (var trip in trips)
            {
                var info = await GetBasicTripInformation(trip);
                infos.Add(info);
            }

            return infos;
        }
    }
}
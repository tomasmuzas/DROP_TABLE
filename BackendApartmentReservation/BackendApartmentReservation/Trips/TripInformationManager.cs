namespace BackendApartmentReservation.Trips
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Responses;
    using Interfaces;

    public class TripInformationManager : ITripInformationManager
    {
        private readonly ITripRepository _tripRepository;

        public TripInformationManager(
            ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
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
                    IsApartmentRequired = c.LivingPlace != null,
                    IsFlightRequired = c.Flight != null,
                    IsCarRentRequired = c.Car != null
                })
                .ToList();

            return new BasicTripInformationResponse
            {
                TripId = tripId,
                StartTime = trip.DepartureDate,
                EndTime = trip.ReturnDate,

                ChecklistInfos = checklistInformations
            };
        }

        public async Task<IEnumerable<BasicTripInformationResponse>> GetAllTripsInformation()
        {
            var trips = await _tripRepository.GetAllTrips();
            return trips.Select(t => new BasicTripInformationResponse
            {
                StartTime = t.DepartureDate,
                EndTime = t.ReturnDate,
                TripId = t.ExternalTripId
            });
        }
    }
}
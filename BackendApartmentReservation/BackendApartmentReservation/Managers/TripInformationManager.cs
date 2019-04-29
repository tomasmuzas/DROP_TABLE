using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    using System.Linq;
    using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Responses;
    using Repositories;

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

            var officeInfo = new OfficeInfoResponse
            {
                Id = trip.DestinationOffice.ExternalOfficeId,
                Address = trip.DestinationOffice.Address
            };

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
                DestinationOffice = officeInfo,
                ChecklistInfos = checklistInformations
            };
        }

        public async Task<BasicTripInformationResponse> CreateBasicTrip(CreateTripRequest tripRequest)
        {
            var trip = await _tripRepository.CreateTrip(tripRequest);

            var officeInfo = new OfficeInfoResponse
            {
                Id = trip.DestinationOffice.ExternalOfficeId,
                Address = trip.DestinationOffice.Address
            };

            return new BasicTripInformationResponse
            {
                TripId = trip.ExternalTripId,
                StartTime = trip.DepartureDate,
                EndTime = trip.ReturnDate,
                DestinationOffice = officeInfo
            };
        }
    }
}

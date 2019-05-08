using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    using System.Linq;
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
    }
}

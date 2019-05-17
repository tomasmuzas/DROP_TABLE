using System.Collections.Generic;

namespace BackendApartmentReservation.Trips.Interfaces
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;

    public interface ITripManager
    {
        Task<TripCreatedResponse> CreateBasicTrip(CreateTripRequest tripRequest, string managerId);
        Task<bool> IsPossibleToMergeTrips(string firstTripId, string secondTripId);
        Task<List<string>> GetAllMergeableTrips(string tripId);
    }
}
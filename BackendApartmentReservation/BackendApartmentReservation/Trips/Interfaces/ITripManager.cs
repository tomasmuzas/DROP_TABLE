using System.Collections.Generic;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Trips.Interfaces
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;

    public interface ITripManager
    {
        Task<TripCreatedResponse> CreateBasicTrip(CreateTripRequest tripRequest, string managerId);
        Task<bool> IsPossibleToMergeTrips(string firstTripId, string secondTripId);
        bool IsPossibleToMergeTrips(DbTrip firstTrip, DbTrip secondTrip);
        Task<IEnumerable<BasicTripInformationResponse>> GetAllMergeableTrips(string tripId);
        Task<TripCreatedResponse> MergeTrips(MergeTripsRequest mergeTripsRequest, string managerId);
    }
}
namespace BackendApartmentReservation.Trips.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;

    public interface ITripInformationManager
    {
        Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId);

        Task<IEnumerable<BasicTripInformationResponse>> GetAllTripsInformation();
    }
}
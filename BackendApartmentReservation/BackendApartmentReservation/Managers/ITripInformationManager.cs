namespace BackendApartmentReservation.Managers
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;

    public interface ITripInformationManager
    {
        Task<BasicTripInformationResponse> GetBasicTripInformation(string tripId);
    }
}
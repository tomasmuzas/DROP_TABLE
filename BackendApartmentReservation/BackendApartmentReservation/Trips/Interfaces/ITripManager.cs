namespace BackendApartmentReservation.Trips.Interfaces
{
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Requests;

    public interface ITripManager
    {
        Task<string> CreateBasicTrip(CreateTripRequest tripRequest);
    }
}
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public interface ITripManager
    {
        Task<string> CreateBasicTrip(CreateTripRequest tripRequest);
    }
}
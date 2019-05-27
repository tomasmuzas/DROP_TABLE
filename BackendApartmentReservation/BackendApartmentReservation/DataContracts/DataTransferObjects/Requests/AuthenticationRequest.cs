using BackendApartmentReservation.Infrastructure.Utilities;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }

        public Password Password { get; set; }
    }
}
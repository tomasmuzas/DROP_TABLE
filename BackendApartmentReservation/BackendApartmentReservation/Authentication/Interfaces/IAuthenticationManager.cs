using System.Threading.Tasks;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;

namespace BackendApartmentReservation.Authentication.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<EmployeeAuthenticationInfo> Authenticate(AuthenticationRequest request);
    }
}

using System.Threading.Tasks;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;

namespace BackendApartmentReservation.Authentication.Interfaces
{
    using Database.Entities;
    using Infrastructure.Utilities;

    public interface IAuthenticationManager
    {
        Task<EmployeeAuthenticationInfo> Authenticate(AuthenticationRequest request);

        Task CreateAuthenticationInfo(DbEmployee dbEmployee, Password password);

        Task<DbEmployee> GetAndVerifyEmployee(string employeeId);
    }
}

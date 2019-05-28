using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Employees;

namespace BackendApartmentReservation.Authentication.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<DbAuthorization> Authorize(string email, string hashedPassword);

        Task CreateAuthenticationInfo(string hashedPassword, DbEmployee employee);

        Task ChangeUserInfo(ChangeUserInfoRequest changeUserInfoRequest);
    }
}

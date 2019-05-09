using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Authentication.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<DbAuthorization> Authorize(string email, string hashedPassword);
    }
}

using System.Threading.Tasks;

using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Repositories
{
    public interface IEmployeeRepository
    {
        Task CreateEmployee(string firstName, string lastName, string email, string office);

        Task SaveChanges();

        Task<DbEmployee> GetEmployeeById(int id);
    }
}
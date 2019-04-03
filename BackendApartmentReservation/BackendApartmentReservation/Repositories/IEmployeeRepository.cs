using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IEmployeeRepository
    {
        Task CreateEmployee(string firstName, string lastName, string email, string office);

        Task SaveChanges();
    }
}
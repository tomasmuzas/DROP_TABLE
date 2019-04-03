using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public interface IEmployeeManager
    {
        Task<string> CreateEmployee(string firstName, string lastName, string email, string office);
    }
}
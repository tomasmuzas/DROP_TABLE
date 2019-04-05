using BackendApartmentReservation.Database.Entities;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public interface IEmployeeManager
    {
        Task<string> CreateEmployee(DbEmployee dbEmployee);
    }
}
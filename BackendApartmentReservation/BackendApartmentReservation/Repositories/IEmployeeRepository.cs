using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<DbEmployee>> GetAllEmployees();
        Task<DbEmployee> GetEmployeeById(int employeeID);
        Task<DbEmployee> GetEmployeeById(string employeeID);
        Task CreateEmployee(DbEmployee dbEmployee);
        Task UpdateEmployee(DbEmployee dbEmployee);
        Task DeleteEmployee(DbEmployee dbEmployee);
    }
}
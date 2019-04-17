namespace BackendApartmentReservation.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IEmployeeRepository
    {
        Task<IEnumerable<DbEmployee>> GetAllEmployees();

        Task<DbEmployee> GetEmployeeById(int employeeID);

        Task CreateEmployee(DbEmployee dbEmployee);

        Task UpdateEmployee(DbEmployee dbEmployee);

        Task DeleteEmployee(DbEmployee dbEmployee);
    }
}
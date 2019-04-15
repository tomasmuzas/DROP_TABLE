using BackendApartmentReservation.Database.Entities;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    using System.Collections.Generic;
    using DataContracts.DataTransferObjects.Responses;

    public interface IEmployeeManager
    {
        Task<string> CreateEmployee(DbEmployee dbEmployee);

        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();

        Task<EmployeeInfo> GetEmployeeById(string employeeID);
    }
}
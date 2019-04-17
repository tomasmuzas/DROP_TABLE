namespace BackendApartmentReservation.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using DataContracts.DataTransferObjects.Responses;

    public interface IEmployeeManager
    {
        Task<string> CreateEmployee(DbEmployee dbEmployee);

        Task<IEnumerable<GetUserResponse>> GetAllEmployees();
    }
}
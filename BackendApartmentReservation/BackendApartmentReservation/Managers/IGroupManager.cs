using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public interface IGroupManager
    {
        Task<string> CreateGroup(string officeID, List<DbEmployee> employees);
        Task AddEmployeeToGroup(string groupID, string employeeID);
    }
}
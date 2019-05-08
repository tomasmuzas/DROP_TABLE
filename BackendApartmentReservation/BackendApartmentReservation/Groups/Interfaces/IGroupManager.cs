namespace BackendApartmentReservation.Groups.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IGroupManager
    {
        Task<string> CreateGroup(string officeID, List<DbEmployee> employees);
        Task AddEmployeeToGroup(string groupID, string employeeID);
        Task<IEnumerable<DbEmployeeGroup>> GetEmployeeGroupsByGroupId(string groupID);
    }
}
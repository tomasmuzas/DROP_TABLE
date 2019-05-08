using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<DbGroup>> GetAllGroups();
        Task<DbGroup> GetGroupById(string groupID);
        Task CreateGroup(DbGroup dbGroup);
        Task CreateEmployeeGroup(DbEmployeeGroup dbEmployeeGroup);
        Task<IEnumerable<DbEmployeeGroup>> GetEmployeeGroupsByGroupId(string groupID);
    }
}
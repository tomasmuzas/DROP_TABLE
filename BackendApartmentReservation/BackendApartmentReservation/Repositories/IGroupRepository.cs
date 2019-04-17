using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<DbGroup>> GetAllGroups();
        Task<DbGroup> GetGroupById(int groupID);
        Task CreateGroup(DbGroup dbGroup);
        Task AddEmployeeToGroup(int groupID, int emplID);
    }
}
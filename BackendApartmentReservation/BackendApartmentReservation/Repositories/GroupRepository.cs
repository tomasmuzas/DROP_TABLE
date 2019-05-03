using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApartmentReservation.Database;
using BackendApartmentReservation.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseContext _context;

        public GroupRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DbGroup>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<DbGroup> GetGroupById(string groupID)
        {
            return await _context.Groups.SingleOrDefaultAsync(e => e.ExternalGroupId == groupID);
        }

        public async Task CreateGroup(DbGroup dbGroup)
        {
            await _context.Groups.AddAsync(dbGroup);
            await _context.SaveChangesAsync();
        }

        public async Task CreateEmployeeGroup(DbEmployeeGroup dbEmployeeGroup)
        {
            await _context.DbEmployeeGroup.AddAsync(dbEmployeeGroup);
            await _context.SaveChangesAsync();
        }
    }
}

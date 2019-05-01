using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.Groups.FirstOrDefaultAsync(e => e.ExternalGroupId == groupID);
        }

        public async Task CreateGroup(DbGroup dbGroup)
        {
            await _context.Groups.AddAsync(dbGroup);
            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeeToGroup(string groupID, string emplID)
        {
            var group = await _context.Groups
            .Include(p => p.Employees)
            .SingleAsync(p => p.ExternalGroupId == groupID);

            var newEmpl = await _context.Employees
                .SingleAsync(p => p.ExternalEmployeeId == emplID);
            group.Employees.Add(new DbEmployeeGroup
            {
                DbEmployee = newEmpl,
                DbGroup = group
            });

            await _context.SaveChangesAsync();
        }
    }
}

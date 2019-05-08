using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApartmentReservation.Database;
using BackendApartmentReservation.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly DatabaseContext _context;

        public OfficeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DbOffice> GetOfficeById(string officeID)
        {
            return await _context.Offices.SingleOrDefaultAsync(o => o.ExternalOfficeId == officeID);
        }

        public async Task<IEnumerable<DbOffice>> GetAllOffices()
        {
            return await _context.Offices.ToListAsync();
        }
    }
}

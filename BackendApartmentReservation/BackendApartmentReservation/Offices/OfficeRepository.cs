namespace BackendApartmentReservation.Offices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

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
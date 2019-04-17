using System.Threading.Tasks;
using BackendApartmentReservation.Database;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly DatabaseContext _context;

        public OfficeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DbOffice> GetOfficeById(int officeID)
        {
            return await _context.Offices.FindAsync(officeID);
        }
        
    }
}

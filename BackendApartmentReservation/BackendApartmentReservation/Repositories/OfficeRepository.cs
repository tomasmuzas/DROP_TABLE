namespace BackendApartmentReservation.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Microsoft.EntityFrameworkCore;

    public class OfficeRepository : IOfficeRepository
    {
        private readonly DatabaseContext _db;

        public OfficeRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DbOffice>> GetAllOffices()
        {
            return await _db.Offices.ToListAsync();
        }

        public async Task<DbOffice> GetOfficeById(int officeId)
        {
            return await _db.Offices.FirstOrDefaultAsync(o => o.Id == officeId);
        }
    }
}

using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories.Checklist
{
    using Database;
    using Database.Entities;

    public class ChecklistRepository : IChecklistRepository
    {
        private readonly DatabaseContext _db;

        public ChecklistRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task AddChecklist(DbEmployeeAmenitiesChecklist checklist)
        {
            await _db.Checklists.AddAsync(checklist);
        }
    }
}

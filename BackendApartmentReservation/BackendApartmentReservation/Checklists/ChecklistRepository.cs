using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories.Checklist
{
    using System.Linq;
    using Database;
    using Database.Entities;
    using Database.Entities.Amenities;
    using Microsoft.EntityFrameworkCore;

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
            await _db.SaveChangesAsync();
        }

        public async Task UpdateChecklist(DbEmployeeAmenitiesChecklist checklist)
        {
            _db.Update(checklist);
            await _db.SaveChangesAsync();
        }

        public async Task<DbEmployeeAmenitiesChecklist> GetChecklist(string employeeId, string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .SingleOrDefaultAsync(c => c.Employee.ExternalEmployeeId == employeeId 
                    && c.Trip.ExternalTripId == tripId);
        }

        public async Task<DbEmployeeAmenitiesChecklist> GetFullChecklist(string employeeId, string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Include(c => c.Flight)
                    .ThenInclude(f => f.FlightReservation)
                .Include(c => c.Car)
                    .ThenInclude(car => car.CarReservation)
                .Include(c => c.LivingPlace)
                    .ThenInclude(l => l.LivingPlaceReservation)
                .SingleOrDefaultAsync(c => c.Employee.ExternalEmployeeId == employeeId
                    && c.Trip.ExternalTripId == tripId);
        }

        public async Task<DbFlightAmenity> GetChecklistFullFlight(string employeeId, string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Where(c => c.Employee.ExternalEmployeeId == employeeId
                    && c.Trip.ExternalTripId == tripId)
                .Select(c => c.Flight)
                    .Include(c => c.FlightReservation)
                .SingleOrDefaultAsync();
        }
    }
}

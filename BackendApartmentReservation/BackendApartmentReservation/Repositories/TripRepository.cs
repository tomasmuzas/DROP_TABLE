using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Database;
    using Database.Entities;
    using Microsoft.EntityFrameworkCore;

    public class TripRepository : ITripRepository
    {
        private readonly DatabaseContext _db;

        public TripRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbTrip> GetTrip (string tripId)
        {
            return await _db.Trips
                .Where(t => t.ExternalTripId == tripId)
                .SingleAsync();
        }

        public async Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetTripChecklistsWithEmployees(string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Employee)
                .Where(c => c.Trip.ExternalTripId == tripId)
                .Include(c => c.Car)
                .Include(c => c.Flight)
                .Include(c => c.LivingPlace)
                .ToListAsync();
        }
    }
}

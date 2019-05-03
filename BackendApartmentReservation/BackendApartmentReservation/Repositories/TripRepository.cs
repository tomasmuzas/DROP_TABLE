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

        public async Task<DbTrip> CreateTrip (CreateTripRequest tripRequest)
        {
            var destinationOffice = await _db.Offices.SingleOrDefaultAsync(o => o.ExternalOfficeId == tripRequest.DestinationOfficeId);
            var employees = await _db.Employees
                .Include(e => e.Office)
                .Where(e => tripRequest.UserIds.Contains(e.ExternalEmployeeId))
                .ToListAsync();

            var groups = (from employee in employees
                          group employee by employee.Office.ExternalOfficeId into g
                          join office in _db.Offices on g.FirstOrDefault().Office.ExternalOfficeId equals office.ExternalOfficeId
                          select new DbGroup
                          {
                              Employees = g.ToList(),
                              StartingOffice = g.FirstOrDefault().Office
                          }).ToList();

            var newTrip = new DbTrip
            {
                DepartureDate = tripRequest.DepartureDate,
                DestinationOffice = destinationOffice,
                ReturnDate = tripRequest.ReturnDate,
                Groups = groups
            };

            await _db.Groups.AddRangeAsync(groups);
            await _db.Trips.AddAsync(newTrip);
            await _db.SaveChangesAsync();

            return newTrip;
        }
    }
}

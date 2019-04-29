using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
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
            if (tripRequest.DepartureDate != null && tripRequest.ReturnDate != null)
            {
                var destinationOffice = await _db.Offices.FirstOrDefaultAsync(o => o.Id == tripRequest.DestinationOfficeId);
                var employees = await _db.Employees
                    .Include(e => e.Office)
                    .Where(e => tripRequest.UserIds.Contains(e.Id))
                    .ToListAsync();

                var groups = (from e in employees
                             group e by e.Office into g
                             select new DbGroup
                             {
                                 Employees = g.ToList(),
                                 StartingOffice = g.Key
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
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}

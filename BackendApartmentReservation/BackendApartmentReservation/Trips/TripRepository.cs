namespace BackendApartmentReservation.Trips
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using DataContracts.DataTransferObjects.Requests;
    using Interfaces;
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
            var tripGroups = new List<DbGroup>();
            var destinationOffice = await _db.Offices.SingleOrDefaultAsync(o => o.ExternalOfficeId == tripRequest.DestinationOfficeId);
            var employees = await _db.Employees
                .Include(e => e.Office)
                .Where(e => tripRequest.EmployeeIds.Contains(e.ExternalEmployeeId))
                .ToListAsync();

            var employeesGroups = employees.GroupBy(e => e.Office.ExternalOfficeId);
            foreach (var employeesGroup in employeesGroups)
            {
                var tripGroup = new DbGroup
                {
                    StartingOffice = await _db.Offices.SingleOrDefaultAsync(o => o.ExternalOfficeId == employeesGroup.Key)
                };
                tripGroups.Add(tripGroup);
                await _db.Groups.AddAsync(tripGroup);
                
                var employeeGroups = employeesGroup.Select(e => new DbEmployeeGroup
                {
                    DbEmployee = _db.Employees.SingleOrDefault(x => x.ExternalEmployeeId == e.ExternalEmployeeId),
                    DbGroup = tripGroup
                }).ToList();

                await _db.DbEmployeeGroup.AddRangeAsync(employeeGroups);
            }

            var newTrip = new DbTrip
            {
                DepartureDate = tripRequest.DepartureDate,
                DestinationOffice = destinationOffice,
                ReturnDate = tripRequest.ReturnDate,
                Groups = tripGroups
            };

            await _db.Trips.AddAsync(newTrip);
            await _db.SaveChangesAsync();

            return newTrip;
        }
    }
}

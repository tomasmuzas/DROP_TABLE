using BackendApartmentReservation.Infrastructure.Exceptions;

namespace BackendApartmentReservation.Trips
{
    using System;
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

        public async Task<DbTrip> GetTrip(string tripId)
        {
            try
            {
                return await _db.Trips
                    .Where(t => t.ExternalTripId == tripId)
                    .SingleAsync();
            }
            catch (InvalidOperationException)
            {
                throw new ErrorCodeException(ErrorCodes.TripNotFound);
            }
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

        public async Task<DbTrip> CreateTrip(CreateTripRequest tripRequest, string managerId)
        {
            var tripGroups = new List<DbGroup>();

            DbOffice destinationOffice;
            try
            {
                destinationOffice =
                    await _db.Offices.SingleOrDefaultAsync(o => o.ExternalOfficeId == tripRequest.DestinationOfficeId);
            }
            catch (InvalidOperationException)
            {
                throw new ErrorCodeException(ErrorCodes.TripOfficeNotFound);
            }

            var employees = await _db.Employees
                .Include(e => e.Office)
                .Where(e => tripRequest.EmployeeIds.Contains(e.ExternalEmployeeId))
                .ToListAsync();

            var employeesGroups = employees.GroupBy(e => e.Office.ExternalOfficeId);
            foreach (var employeesGroup in employeesGroups)
            {
                var tripGroup = new DbGroup
                {
                    StartingOffice =
                        await _db.Offices.SingleOrDefaultAsync(o => o.ExternalOfficeId == employeesGroup.Key),
                    ExternalGroupId = Guid.NewGuid().ToString()
                };
                tripGroups.Add(tripGroup);
                await _db.Groups.AddAsync(tripGroup);

                var employeeGroups = employeesGroup.Select(e => new DbEmployeeGroup
                    {
                        DbEmployee = _db.Employees.SingleOrDefault(x => x.ExternalEmployeeId == e.ExternalEmployeeId),
                        DbGroup = tripGroup
                    })
                    .ToList();

                await _db.DbEmployeeGroup.AddRangeAsync(employeeGroups);
            }

            var tripCreator = await _db.Employees.SingleAsync(e => e.ExternalEmployeeId == managerId);

            var newTrip = new DbTrip
            {
                DepartureDate = tripRequest.DepartureDate,
                DestinationOffice = destinationOffice,
                ReturnDate = tripRequest.ReturnDate,
                Groups = tripGroups,
                TripCreator = tripCreator,
                ExternalTripId = Guid.NewGuid().ToString()
            };

            await _db.Trips.AddAsync(newTrip);
            await _db.SaveChangesAsync();

            return newTrip;
        }

        public async Task<IEnumerable<DbTrip>> GetAllOrganizedTripsOfEmployee(string employeeId)
        {
            return await _db.Trips
                .Where(t => t.TripCreator.ExternalEmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<DbTrip>> GetAllParticipatingTripsOfEmployee(string employeeId)
        {
            var groups = await _db.DbEmployeeGroup
                .Include(eg => eg.DbEmployee)
                .Include(eg => eg.DbGroup)
                .Where(eg => eg.DbEmployee.ExternalEmployeeId == employeeId)
                .Select(eg => eg.DbGroup)
                .ToListAsync();

            return await _db.Trips
                .Where(t => t.Groups
                    .Any(g => groups.Contains(g)))
                .ToListAsync();
        }

        public async Task<IEnumerable<DbTrip>> GetAllTrips()
        {
            return await _db.Trips
                .ToListAsync();
        }

        public async Task DeleteTrip(string tripId)
        {
            var trip = await _db.Trips.Where(t => t.ExternalTripId == tripId).SingleOrDefaultAsync();
            if (trip == null)
            {
                throw new ErrorCodeException(ErrorCodes.TripNotFound);
            }

            var checklists = await _db.Checklists.Where(c => c.Trip.ExternalTripId == tripId).ToListAsync();
            foreach (var checklist in checklists)
            {
                if (checklist.Car != null)
                {
                    _db.CarReservations.Remove(checklist.Car.CarReservation);
                    _db.CarRentAmenities.Remove(checklist.Car);
                }

                if (checklist.Flight != null)
                {
                    _db.FlightReservations.Remove(checklist.Flight.FlightReservation);
                    _db.FlightAmenities.Remove(checklist.Flight);
                }

                if (checklist.LivingPlace.ApartmentRoomReservation != null)
                    _db.DbRoomReservations.Remove(checklist.LivingPlace.ApartmentRoomReservation);

                if (checklist.LivingPlace.HotelReservation != null)
                    _db.HotelReservations.Remove(checklist.LivingPlace.HotelReservation);

                _db.LivingPlaceAmenities.Remove(checklist.LivingPlace);
                _db.Checklists.Remove(checklist);
            }

            var groups = await _db.Groups.Where(g => trip.Groups.Any(tg => tg.ExternalGroupId == g.ExternalGroupId)).ToListAsync();
            foreach (var group in groups)
            {
                var employeeGroups = _db.DbEmployeeGroup.Where(eg => eg.DbGroup.ExternalGroupId == group.ExternalGroupId);
                _db.DbEmployeeGroup.RemoveRange(employeeGroups);
            }

            _db.Groups.RemoveRange(groups);

            _db.Trips.Remove(trip);
            await _db.SaveChangesAsync();
        }
    }
}
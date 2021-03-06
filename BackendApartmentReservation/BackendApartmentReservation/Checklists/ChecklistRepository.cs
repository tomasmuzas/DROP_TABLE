﻿namespace BackendApartmentReservation.Checklists
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Database.Entities.Reservations;
    using Database;
    using Database.Entities;
    using Database.Entities.Amenities;
    using Interfaces;
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
                .SingleOrDefaultAsync(c =>
                    c.Employee.ExternalEmployeeId == employeeId && c.Trip.ExternalTripId == tripId);
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
                    .ThenInclude(l => l.ApartmentRoomReservation)
                .Include(l => l.LivingPlace.HotelReservation)
                .SingleOrDefaultAsync(c =>
                    c.Employee.ExternalEmployeeId == employeeId && c.Trip.ExternalTripId == tripId);
        }

        public async Task<DbFlightAmenity> GetChecklistFullFlight(string employeeId, string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Where(c => c.Employee.ExternalEmployeeId == employeeId && c.Trip.ExternalTripId == tripId)
                .Select(c => c.Flight)
                .Include(c => c.FlightReservation)
                .Include(c => c.Ticket)
                .SingleOrDefaultAsync();
        }

        public async Task<DbCarRentAmenity> GetChecklistFullCarRent(string employeeId, string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Where(c => c.Employee.ExternalEmployeeId == employeeId && c.Trip.ExternalTripId == tripId)
                .Select(c => c.Car)
                .Include(c => c.CarReservation)
                .Include(c => c.Documents)
                .SingleOrDefaultAsync();
        }

        public async Task<DbHotelReservation> GetChecklistFullHotelReservation(string employeeId, string tripId)
        {
            var livingPlace = await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Where(c => c.Employee.ExternalEmployeeId == employeeId && c.Trip.ExternalTripId == tripId)
                .Select(c => c.LivingPlace)
                .Include(c => c.HotelReservation)
                    .ThenInclude(h => h.Documents)
                .SingleOrDefaultAsync();

            return livingPlace?.HotelReservation;
        }

        public async Task<DbRoomReservation> GetChecklistFullApartmentRoomReservation(string employeeId, string tripId)
        {
            var livingPlace = await _db.Checklists
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Where(c => c.Employee.ExternalEmployeeId == employeeId && c.Trip.ExternalTripId == tripId)
                .Select(c => c.LivingPlace)
                .Include(c => c.ApartmentRoomReservation)
                    .ThenInclude(a => a.Room)
                .SingleOrDefaultAsync();

            return livingPlace?.ApartmentRoomReservation;
        }

        public async Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetAllTripChecklists(string tripId)
        {
            return await _db.Checklists
                .Include(c => c.Trip)
                .Where(c => c.Trip.ExternalTripId == tripId)
                .Include(c => c.Employee)
                .ToListAsync();
        }
    }
}
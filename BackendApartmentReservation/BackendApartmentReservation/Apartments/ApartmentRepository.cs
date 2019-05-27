using System.Net.Sockets;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;

namespace BackendApartmentReservation.Apartments
{
    using BackendApartmentReservation.Database.Entities;
    using Database;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using BackendApartmentReservation.Infrastructure.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DatabaseContext _db;

        public ApartmentRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbApartment> CreateApartment(string address, List<DbApartmentRoom> apartmentRooms)
        {
            var apartment = new DbApartment
            {
                Address = address,
                Rooms = apartmentRooms
            };

            await _db.Apartments.AddAsync(apartment);
            await _db.SaveChangesAsync();
            return apartment;
        }

        public async Task<DbRoomReservation> CreateRoomReservation(string tripId, DbEmployee employee, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var roomReservation = new DbRoomReservation();
            roomReservation.Employee = employee;
            roomReservation.DateFrom = dateFrom;
            roomReservation.DateTo = dateTo;

            var availableRooms = await GetAvailableRooms(tripId, dateFrom, dateTo);

            roomReservation.Room = availableRooms.FirstOrDefault();
            if (roomReservation.Room == null)
            {
                throw new ErrorCodeException(ErrorCodes.NoMoreApartmentRoomsLeft);
            }

            await _db.DbRoomReservations.AddAsync(roomReservation);
            await _db.SaveChangesAsync();

            return roomReservation;
        }

        public async Task UpdateRoomReservation(DbRoomReservation roomReservation)
        {
            _db.DbRoomReservations.Update(roomReservation);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRoomReservation(DbRoomReservation roomReservation)
        {
            _db.DbRoomReservations.Remove(roomReservation);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<DbApartmentRoom>> GetAvailableRooms(string tripId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var trip = await _db.Trips
                .Where(t => t.ExternalTripId == tripId)
                .Include(t => t.DestinationOffice)
                    .ThenInclude(o => o.OfficeApartment)
                        .ThenInclude(a => a.Rooms)
                .SingleOrDefaultAsync();

            var office = trip.DestinationOffice;

            var rooms = office.OfficeApartment.Rooms;

            var availableRooms = new List<DbApartmentRoom>();
            foreach (var room in rooms)
            {
                var isAvailable = !_db.DbRoomReservations
                    .Where(r => r.Room.Id == room.Id)
                    .Any(r => Math.Max(dateFrom.Date.Ticks, r.DateFrom.Date.Ticks) <=
                              Math.Min(dateTo.Date.Ticks, r.DateTo.Date.Ticks));
                if (isAvailable)
                {
                    availableRooms.Add(room);
                }
            }

            return availableRooms;
        }

        public async Task<DbRoomReservation> GetRoomReservation(int reservationId)
        {
            return await _db.DbRoomReservations.FindAsync(reservationId);
        }

        public async Task<IEnumerable<DbApartment>> GetAllApartments()
        {
            return await _db.Apartments.ToListAsync();
        }
    }
}
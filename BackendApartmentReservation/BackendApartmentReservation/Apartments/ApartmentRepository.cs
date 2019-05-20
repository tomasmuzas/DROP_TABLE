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

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DatabaseContext _db;

        public ApartmentRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbRoomReservation> CreateRoomReservation(DbEmployee employee, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var roomReservation = new DbRoomReservation();
            roomReservation.Employee = employee;
            roomReservation.DateFrom = dateFrom;
            roomReservation.DateTo = dateTo;

            var availableRooms = new List<DbApartmentRoom>();
            foreach (DbApartmentRoom room in _db.ApartmentRooms)
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

        public async Task<int> GetNumberOfAvailableApartmentsRooms(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var availableRooms = (from room in _db.ApartmentRooms
                let isAvailable = !_db.DbRoomReservations.Where(r => r.Room.Id == room.Id)
                    .Any(r => Math.Max(dateFrom.Date.Ticks, r.DateFrom.Date.Ticks) <= Math.Min(dateTo.Date.Ticks, r.DateTo.Date.Ticks))
                where isAvailable
                select room).ToList();

            return availableRooms.Count;
        }
        
    }
}
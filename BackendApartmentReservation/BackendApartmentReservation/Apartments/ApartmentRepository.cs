namespace BackendApartmentReservation.Apartments
{
    using BackendApartmentReservation.Database.Entities;
    using Database;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DatabaseContext _db;

        public ApartmentRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbRoomReservation> CreateRoomReservation(DbEmployee employee, DateTime dateFrom, DateTime dateTo)
        {
            var roomReservation = new DbRoomReservation();
            roomReservation.Employee = employee;
            roomReservation.DateFrom = dateFrom;
            roomReservation.DateTo = dateTo;

            var availableRooms = new List<DbApartmentRoom>();
            foreach (DbApartmentRoom room in _db.ApartmentRooms)
            {
                var isAvailable = _db.DbRoomReservations
                    .Where(e => e.Room.Id == room.Id)
                    .All(e => e.DateFrom.Date > dateTo.Date || e.DateTo.Date < dateFrom.Date);
                if (isAvailable)
                    availableRooms.Add(room);
            }

            roomReservation.Room = availableRooms.FirstOrDefault();
            if (roomReservation.Room == null)
                throw new ArgumentNullException();

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
    }
}
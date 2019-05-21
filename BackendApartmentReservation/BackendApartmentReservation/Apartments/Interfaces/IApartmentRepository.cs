using System.Collections.Generic;

namespace BackendApartmentReservation.Apartments.Interfaces
{
    using BackendApartmentReservation.Database.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IApartmentRepository
    {
        Task<DbRoomReservation> CreateRoomReservation(DbEmployee employee, DateTimeOffset dateFrom, DateTimeOffset dateTo);

        Task UpdateRoomReservation(DbRoomReservation roomReservation);

        Task DeleteRoomReservation(DbRoomReservation roomReservation);

        Task<IEnumerable<DbApartmentRoom>> GetAvailableRooms(string tripId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
    }
}
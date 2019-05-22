using System.Collections.Generic;

namespace BackendApartmentReservation.Apartments.Interfaces
{
    using Database.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IApartmentRepository
    {
        Task<DbRoomReservation> CreateRoomReservation(string tripId, DbEmployee employee, DateTimeOffset dateFrom, DateTimeOffset dateTo);

        Task UpdateRoomReservation(DbRoomReservation roomReservation);

        Task DeleteRoomReservation(DbRoomReservation roomReservation);

        Task<IEnumerable<DbApartmentRoom>> GetAvailableRooms(string tripId, DateTimeOffset dateFrom, DateTimeOffset dateTo);

        Task<DbRoomReservation> GetRoomReservation(int reservationId);
    }
}
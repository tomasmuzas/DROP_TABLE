namespace BackendApartmentReservation.Apartments.Interfaces
{
    using BackendApartmentReservation.Database.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IApartmentRepository
    {
        Task<DbRoomReservation> CreateRoomReservation(DbEmployee employee, DateTime dateFrom, DateTime dateTo);

        Task UpdateRoomReservation(DbRoomReservation roomReservation);

        Task DeleteRoomReservation(DbRoomReservation roomReservation);
    }
}
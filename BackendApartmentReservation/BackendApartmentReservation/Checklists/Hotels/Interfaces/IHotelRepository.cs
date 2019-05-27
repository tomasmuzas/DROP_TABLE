namespace BackendApartmentReservation.Checklists.Hotels.Interfaces
{
    using BackendApartmentReservation.Database.Entities.Reservations;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHotelRepository
    {
        Task<DbHotelReservation> CreateEmptyHotelReservation();
        Task UpdateHotelReservation(DbHotelReservation hotelReservation);
        Task<IEnumerable<DbHotelReservation>> GetAllHotelReservations();
        Task DeleteHotelReservation(DbHotelReservation hotelReservation);
        Task<DbHotelReservation> GetHotelReservationById(int hotelReservationId);
    }
}
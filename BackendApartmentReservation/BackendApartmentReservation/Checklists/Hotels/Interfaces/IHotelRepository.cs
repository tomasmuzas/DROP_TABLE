namespace BackendApartmentReservation.Checklists.Hotels.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface IHotelRepository
    {
        Task<DbHotelAmenity> CreateHotelAmenity(string address);
    }
}

namespace BackendApartmentReservation.Checklists.Hotels.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface ILivingPlaceRepository
    {
//        Task<DbLivingPlaceAmenity> CreateLivingPlaceAmenity(
//            DbApartmentAmenity apartmentAmenity,
//            DbHotelAmenity hotelAmenity);

        Task<DbApartmentAmenity> CreateApartmentAmenity(string address);

        Task<DbHotelAmenity> CreateHotelAmenity(string address);
    }
}
namespace BackendApartmentReservation.Checklists.Hotels.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface ILivingPlaceRepository
    {
        Task<DbLivingPlaceAmenity> CreateEmptyLivingPlaceAmenity();

        //Task<DbLivingPlaceAmenity> CreateApartmentAmenity(string address);

        Task<DbLivingPlaceAmenity> CreateHotelLivingPlaceAmenity(int hotelReservationId);
    }
}
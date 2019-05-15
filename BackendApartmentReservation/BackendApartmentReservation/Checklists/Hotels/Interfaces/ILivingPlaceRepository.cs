namespace BackendApartmentReservation.Checklists.Hotels.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface ILivingPlaceRepository
    {
        Task<DbLivingPlaceAmenity> CreateEmptyLivingPlaceAmenity();

//        Task<DbApartmentAmenity> CreateApartmentAmenity(string address);
//
//        Task<DbHotelAmenity> CreateHotelAmenity(string address);
    }
}
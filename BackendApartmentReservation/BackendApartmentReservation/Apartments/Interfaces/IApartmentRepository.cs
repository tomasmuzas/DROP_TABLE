namespace BackendApartmentReservation.Apartments.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface IApartmentRepository
    {
        Task<DbApartmentAmenity> CreateApartmentAmenity(string address);
    }
}
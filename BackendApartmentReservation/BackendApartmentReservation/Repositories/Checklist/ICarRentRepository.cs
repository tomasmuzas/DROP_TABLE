namespace BackendApartmentReservation.Repositories.Checklist
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface ICarRentRepository
    {
        Task<DbCarRentAmenity> CreateCarRentAmenityFromCarNumber(string carNumber);
    }
}
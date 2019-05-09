namespace BackendApartmentReservation.Checklists.Cars.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface ICarRentRepository
    {
        Task<DbCarRentAmenity> CreateCarRentAmenityFromCarNumber(string carNumber);
    }
}
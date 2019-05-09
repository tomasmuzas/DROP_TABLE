namespace BackendApartmentReservation.Checklists.Cars.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface ICarRentRepository
    {
        Task<DbCarRentAmenity> CreateEmptyCarRent();

        Task UpdateCarRent(DbCarRentAmenity carRent);

        Task DeleteCarRent(DbCarRentAmenity carRent);
    }
}
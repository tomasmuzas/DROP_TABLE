namespace BackendApartmentReservation.Checklists.Flights.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface IFlightRepository
    {
        /// <summary>
        /// Creates flight amenity from a flight number.
        /// </summary>
        /// <returns></returns>
        Task<DbFlightAmenity> CreateEmptyFlight();

        Task UpdateFlight(DbFlightAmenity flight);

        Task DeleteFlight(DbFlightAmenity flight);
    }
}
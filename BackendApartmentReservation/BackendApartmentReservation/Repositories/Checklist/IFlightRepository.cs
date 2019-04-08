namespace BackendApartmentReservation.Repositories.Checklist
{
    using System.Threading.Tasks;
    using Database.Entities.Amenities;

    public interface IFlightRepository
    {
        /// <summary>
        /// Creates flight amenity from a flight number.
        /// Default flight reservation will be created if no reservation with given <paramref name="flightNumber"/> exists.
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        Task<DbFlightAmenity> CreateFlightAmenityFromFlightNumber(string flightNumber);
    }
}

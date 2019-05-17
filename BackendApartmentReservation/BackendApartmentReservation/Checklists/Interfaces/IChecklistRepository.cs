namespace BackendApartmentReservation.Checklists.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities.Reservations;
    using Database.Entities;
    using Database.Entities.Amenities;

    public interface IChecklistRepository
    {
        Task AddChecklist(DbEmployeeAmenitiesChecklist checklist);

        Task UpdateChecklist(DbEmployeeAmenitiesChecklist checklist);

        Task<DbEmployeeAmenitiesChecklist> GetChecklist(string employeeId, string tripId);

        Task<DbEmployeeAmenitiesChecklist> GetFullChecklist(string employeeId, string tripId);

        Task<DbFlightAmenity> GetChecklistFullFlight(string employeeId, string tripId);

        Task<DbCarRentAmenity> GetChecklistFullCarRent(string employeeId, string tripId);

        Task<DbHotelReservation> GetChecklistFullHotelReservation(string employeeId, string tripId);
    }
}
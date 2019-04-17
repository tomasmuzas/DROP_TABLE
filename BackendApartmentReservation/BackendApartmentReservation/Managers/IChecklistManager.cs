namespace BackendApartmentReservation.Managers
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IChecklistManager
    {
        Task<DbEmployeeAmenitiesChecklist> CreateChecklistForEmployee(
            int employeeId,
            FlightReservationInfo flightReservationInfo,
            CarReservationInfo carReservationInfo);
    }
}
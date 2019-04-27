using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Managers
{
    public interface IChecklistManager
    {
        Task<DbEmployeeAmenitiesChecklist> CreateChecklistForEmployee(
            int employeeId, 
            FlightReservationInfo flightReservationInfo, 
            CarReservationInfo carReservationInfo, 
            LivingPlaceReservationInfo livingPlaceReservationInfo);

    }
}
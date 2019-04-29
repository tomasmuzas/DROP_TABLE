using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Managers
{
    public interface IChecklistManager
    {
        Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(string employeeId, string tripId);
    }
}
namespace BackendApartmentReservation.Repositories.Checklist
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IChecklistRepository
    {
        Task AddChecklist(DbEmployeeAmenitiesChecklist checklist);

        Task UpdateChecklist(DbEmployeeAmenitiesChecklist checklist);

        Task<DbEmployeeAmenitiesChecklist> GetChecklist(string employeeId, string tripId);

        Task<DbEmployeeAmenitiesChecklist> GetFullChecklist(string employeeId, string tripId);
    }
}

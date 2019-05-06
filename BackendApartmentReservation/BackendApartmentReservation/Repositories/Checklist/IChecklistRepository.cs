namespace BackendApartmentReservation.Repositories.Checklist
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IChecklistRepository
    {
        Task AddChecklist(DbEmployeeAmenitiesChecklist checklist);
    }
}

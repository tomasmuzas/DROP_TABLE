namespace BackendApartmentReservation.Managers
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IFlightManager
    {
        Task AddEmptyFlightToChecklist(DbEmployeeAmenitiesChecklist checklist);
    }
}
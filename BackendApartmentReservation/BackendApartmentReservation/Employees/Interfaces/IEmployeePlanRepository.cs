namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IEmployeePlanRepository
    {
        Task<DbEmployeePlan> CreateEmployeePlan(DbEmployeePlan dbEmployeePlan);
    }
}
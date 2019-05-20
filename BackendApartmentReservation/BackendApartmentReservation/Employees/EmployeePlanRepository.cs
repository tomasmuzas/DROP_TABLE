namespace BackendApartmentReservation.Employees
{
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Interfaces;

    public class EmployeePlanRepository : IEmployeePlanRepository
    {
        private readonly DatabaseContext _context;

        public EmployeePlanRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DbEmployeePlan> CreateEmployeePlan(DbEmployeePlan dbEmployeePlan)
        {
            await _context.EmployeePlans.AddAsync(dbEmployeePlan);
            await _context.SaveChangesAsync();
            return dbEmployeePlan;
        }
    }
}
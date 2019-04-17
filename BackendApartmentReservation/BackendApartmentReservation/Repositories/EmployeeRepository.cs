namespace BackendApartmentReservation.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;

        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DbEmployee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<DbEmployee> GetEmployeeById(int employeeID)
        {
            return await _context.Employees.FindAsync(employeeID);
        }

        public async Task CreateEmployee(DbEmployee dbEmployee)
        {
            if (dbEmployee.FirstName != null && dbEmployee.LastName != null && dbEmployee.Email != null)
            {
                await _context.Employees.AddAsync(dbEmployee);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task UpdateEmployee(DbEmployee dbEmployee)
        {
            _context.Employees.Update(dbEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(DbEmployee dbEmployee)
        {
            _context.Employees.Remove(dbEmployee);
            await _context.SaveChangesAsync();
        }
    }
}
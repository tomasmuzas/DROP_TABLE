using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database;
using BackendApartmentReservation.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Repositories
{
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

        public async Task<DbEmployee> GetById(int employeeID)
        {
            return await _context.Employees.FindAsync(employeeID);
        }

        public async Task CreateEmployee(DbEmployee dbEmployee)
        {
            if (dbEmployee.FirstName != null && dbEmployee.LastName != null 
                && dbEmployee.Email != null && dbEmployee.Office != null)
            {
                await _context.Employees.AddAsync(dbEmployee);
                await _context.SaveChangesAsync();
            }  
        }

        public async Task UpdateEmployee(DbEmployee dbEmployee)
        {
            _context.Employees.Update(dbEmployee);
            _context.SaveChanges();
        }

        public async Task DeleteEmployee(DbEmployee dbEmployee)
        {
            _context.Employees.Remove(dbEmployee);
            _context.SaveChanges();
        }

        public async Task SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

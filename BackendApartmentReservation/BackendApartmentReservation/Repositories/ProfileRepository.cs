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
    public class ProfileRepository : IProfileRepository
    {
        private readonly DatabaseContext _context;

        public ProfileRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<DbEmployee> GetAll()
        {
            return _context.Employees;
        }
        
        public DbEmployee GetById(int EmployeeID)
        {
            return _context.Employees.Find(EmployeeID);
        }
        
        public IEnumerable<DbEmployee> GetByFirstNameLastName(string FirstName, string LastName)
        {
            return _context.Employees.Where(e => e.FirstName == FirstName && e.LastName == LastName).ToList();
        }
        
        public DbEmployee Create(DbEmployee DbEmployee)
        {
            if (DbEmployee == null)
            {
                return null;
            }
            _context.Employees.Add(DbEmployee);
            _context.SaveChanges();

            return DbEmployee;
        }

        public bool Update(int id, DbEmployee DbEmployee)
        {
            DbEmployee employeeToUpdate = GetById(id);
            
            if (DbEmployee == null || employeeToUpdate == null)
            {
                return false;
            }
            employeeToUpdate.FirstName = DbEmployee.FirstName;
            employeeToUpdate.LastName = DbEmployee.LastName;
            employeeToUpdate.Office = DbEmployee.Office;

            _context.Employees.Update(employeeToUpdate);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(int EmployeeID)
        {
            DbEmployee employee = _context.Employees.Find(EmployeeID);
            if (employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return true;
        }
    }
}

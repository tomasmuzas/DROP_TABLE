using System;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> CreateEmployee(string firstName, string lastName, string email, string office)
        {
            var employeeId = Guid.NewGuid().ToString();
            DbEmployee dbEmployee = new DbEmployee();
            dbEmployee.FirstName = firstName;
            dbEmployee.LastName = lastName;
            dbEmployee.Email = email;
            dbEmployee.Office = office;

            await _employeeRepository.CreateEmployee(dbEmployee);
            await _employeeRepository.SaveChanges();
            return employeeId;
        }
    }
}

using System;
using System.Threading.Tasks;

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
            await _employeeRepository.CreateEmployee(firstName, lastName, email, office);
            await _employeeRepository.SaveChanges();
            return employeeId;
        }
    }
}

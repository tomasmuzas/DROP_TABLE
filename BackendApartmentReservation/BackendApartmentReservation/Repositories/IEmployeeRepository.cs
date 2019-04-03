﻿using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<DbEmployee>> GetAllEmployees();
        Task<DbEmployee> GetById(int employeeID);
        Task CreateEmployee(DbEmployee dbEmployee);
        Task UpdateEmployee(DbEmployee dbEmployee);
        Task DeleteEmployee(DbEmployee dbEmployee);
        Task SaveChanges();
    }
}
using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;

namespace BackendApartmentReservation.Repositories
{
    public interface IProfileRepository
    {
        IEnumerable<DbEmployee> GetAll();
        DbEmployee GetById(int EmployeeID);
        IEnumerable<DbEmployee> GetByFirstNameLastName(string FirstName, string LastName);
        DbEmployee Create(DbEmployee DbEmployee);
        bool Update(int id, DbEmployee DbEmployee);
        bool Delete(int EmployeeID);
    }
}

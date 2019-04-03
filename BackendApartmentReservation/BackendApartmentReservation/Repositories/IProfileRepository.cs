using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<DbEmployee>> GetAll();
        Task<DbEmployee> GetById(int employeeID);
        Task Create(DbEmployee dbEmployee);
        Task Update(DbEmployee dbEmployee);
        Task Delete(DbEmployee dbEmployee);
        Task SaveChanges();
    }
}

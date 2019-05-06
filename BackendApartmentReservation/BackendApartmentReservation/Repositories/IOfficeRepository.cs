using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IOfficeRepository
    {
        Task<DbOffice> GetOfficeById(int officeID);
        Task<IEnumerable<DbOffice>> GetAllOffices();
    }
}
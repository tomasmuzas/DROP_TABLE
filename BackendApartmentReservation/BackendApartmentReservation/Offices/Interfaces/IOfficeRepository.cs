using BackendApartmentReservation.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Repositories
{
    public interface IOfficeRepository
    {
        Task<DbOffice> GetOfficeById(string officeID);
        Task<IEnumerable<DbOffice>> GetAllOffices();
    }
}
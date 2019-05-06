namespace BackendApartmentReservation.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IOfficeRepository
    {
        Task<IEnumerable<DbOffice>> GetAllOffices();
        Task<DbOffice> GetOfficeById(string officeId);
    }
}
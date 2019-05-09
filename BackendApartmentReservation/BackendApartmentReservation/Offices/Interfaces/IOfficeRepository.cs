namespace BackendApartmentReservation.Offices.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IOfficeRepository
    {
        Task<DbOffice> GetOfficeById(string officeID);

        Task<IEnumerable<DbOffice>> GetAllOffices();
    }
}
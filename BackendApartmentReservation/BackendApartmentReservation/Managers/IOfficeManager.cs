namespace BackendApartmentReservation.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Database.Entities;
    using DataContracts.DataTransferObjects.Responses;

    public interface IOfficeManager
    {
        Task<IEnumerable<OfficeInfoResponse>> GetAllOffices();
        Task<DbOffice> GetOfficeById(string officeId);
    }
}
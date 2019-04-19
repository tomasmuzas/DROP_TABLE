namespace BackendApartmentReservation.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;

    public interface IOfficeManager
    {
        Task<IEnumerable<OfficeInfoResponse>> GetAllOffices();
    }
}
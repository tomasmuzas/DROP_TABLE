namespace BackendApartmentReservation.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;
    using Repositories;

    public class OfficeManager : IOfficeManager
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeManager(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<IEnumerable<OfficeInfoResponse>> GetAllOffices()
        {
            var offices = await _officeRepository.GetAllOffices();

            return offices.Select(o => new OfficeInfoResponse
            {
                Id = o.ExternalOfficeId,
                Address = o.Address
            });
        }

        public async Task<OfficeInfoResponse> GetOfficeById(string officeId)
        {
            var office = await _officeRepository.GetOfficeById(officeId);

            return new OfficeInfoResponse
            {
                Id = office.ExternalOfficeId,
                Address = office.Address
            };
        }
    }
}

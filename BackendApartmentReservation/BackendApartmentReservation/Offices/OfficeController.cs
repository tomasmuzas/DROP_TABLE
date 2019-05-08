namespace BackendApartmentReservation.Offices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataContracts.DataTransferObjects.Responses;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeManager _officeManager;

        public OfficeController(IOfficeManager officeManager)
        {
            _officeManager = officeManager;
        }

        [HttpGet]
        [Route("offices")]
        public async Task<IEnumerable<OfficeInfoResponse>> GetAllOffices()
        {
            return await _officeManager.GetAllOffices();
        }
    }
}
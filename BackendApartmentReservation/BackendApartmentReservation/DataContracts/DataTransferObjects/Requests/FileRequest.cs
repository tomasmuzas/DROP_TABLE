using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using Microsoft.AspNetCore.Http;

    public class FileRequest
    {
        public IFormFile File { get; set; }
    }
}

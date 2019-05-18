using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class CarReservationRequest
    {
        public string CarNumber { get; set; }

        public DateTimeOffset? RentStartTime { get; set; }

        public DateTimeOffset? RentEndTime { get; set; }

        public string CarAddress { get; set; }
    }
}

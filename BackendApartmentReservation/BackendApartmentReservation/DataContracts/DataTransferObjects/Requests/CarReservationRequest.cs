using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class CarReservationRequest
    {
        public string CarNumber { get; set; }

        public DateTime? RentStartTime { get; set; }

        public DateTime? RentEndTime { get; set; }

        public string CarAddress { get; set; }
    }
}

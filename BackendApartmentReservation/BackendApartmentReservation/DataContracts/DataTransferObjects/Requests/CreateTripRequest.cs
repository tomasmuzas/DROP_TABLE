using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class CreateTripRequest
    {
        [Required]
        public int DestinationOfficeId { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public List<int> UserIds { get; set; }
    }
}

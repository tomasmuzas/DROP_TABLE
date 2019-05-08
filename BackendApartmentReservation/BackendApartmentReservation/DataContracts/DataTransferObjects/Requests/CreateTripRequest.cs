using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class CreateTripRequest
    {
        [Required]
        public string DestinationOfficeId { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public List<string> EmployeeIds { get; set; }
    }
}

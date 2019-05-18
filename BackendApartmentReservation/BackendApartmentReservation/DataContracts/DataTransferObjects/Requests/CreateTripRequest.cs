namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateTripRequest
    {
        [Required]
        public string DestinationOfficeId { get; set; }

        [Required]
        public DateTimeOffset DepartureDate { get; set; }

        [Required]
        public DateTimeOffset ReturnDate { get; set; }

        [Required]
        public List<string> EmployeeIds { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class CreateApartmentRequest
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public List<DbApartmentRoom> Rooms { get; set; }

    }
}

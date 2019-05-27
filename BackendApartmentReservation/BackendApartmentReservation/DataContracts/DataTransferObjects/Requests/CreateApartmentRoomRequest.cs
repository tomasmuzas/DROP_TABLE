using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class CreateApartmentRoomRequest
    {
        [Required]
        public int RoomNumber { get; set; }
    }
}

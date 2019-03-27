using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    public class RegisterResponse
    {
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }
    }
}

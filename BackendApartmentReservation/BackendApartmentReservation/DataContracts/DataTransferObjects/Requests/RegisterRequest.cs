using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class RegisterRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@" ^ (?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,15}$")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Office { get; set; }
    }
}

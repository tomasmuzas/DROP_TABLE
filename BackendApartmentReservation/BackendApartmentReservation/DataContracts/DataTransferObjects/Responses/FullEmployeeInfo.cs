using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Employees;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    public class FullEmployeeInfo
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        public string Office { get; set; }

        [Required]
        public EmployeeRole Role { get; set; }
    }
}

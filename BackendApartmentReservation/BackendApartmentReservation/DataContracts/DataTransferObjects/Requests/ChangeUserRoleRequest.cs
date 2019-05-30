using System.ComponentModel.DataAnnotations;
using BackendApartmentReservation.Employees;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

    public class ChangeUserInfoRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        public string OfficeId { get; set; }

        [Required]
        public EmployeeRole Role { get; set; }

        public string Version { get; set; }
    }
}

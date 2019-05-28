using System.ComponentModel.DataAnnotations;
using BackendApartmentReservation.Employees;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class ChangeUserInfoRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public EmployeeRole Role { get; set; }
    }
}

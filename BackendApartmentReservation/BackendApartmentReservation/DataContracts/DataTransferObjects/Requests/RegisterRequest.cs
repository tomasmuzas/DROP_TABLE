namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Employees;
    using Infrastructure.Utilities;

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
        [ValidPassword]
        public Password Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Office { get; set; }

        public EmployeeRole Role { get; set; }
    }
}
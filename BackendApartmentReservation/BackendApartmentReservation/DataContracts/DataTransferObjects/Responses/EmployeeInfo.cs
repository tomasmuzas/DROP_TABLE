using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    public class EmployeeInfo
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Office { get; set; }
    }
}

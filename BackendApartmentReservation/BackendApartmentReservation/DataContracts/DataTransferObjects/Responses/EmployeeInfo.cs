namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeInfo
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
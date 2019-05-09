namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterResponse
    {
        [Required]
        public string Id { get; set; }
    }
}
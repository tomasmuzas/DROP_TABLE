using System.ComponentModel.DataAnnotations;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Responses
{
    public class RegisterResponse
    {
        [Required]
        public string Id { get; set; }
    }
}

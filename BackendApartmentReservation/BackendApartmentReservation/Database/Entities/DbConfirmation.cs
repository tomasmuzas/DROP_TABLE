namespace BackendApartmentReservation.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DbConfirmation
    {
        [Key]
        public int Id { get; set; }

        public string ExternalConfirmationId { get; set; }

        public ConfirmationType Type { get; set; }

        public DbEmployee Employee { get; set; }

        public DateTimeOffset? AcceptedAt { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public DateTimeOffset? DeclinedAt { get; set; }
    }

    public enum ConfirmationType
    {
        TripParticipation,
        TripMerge
    }
}

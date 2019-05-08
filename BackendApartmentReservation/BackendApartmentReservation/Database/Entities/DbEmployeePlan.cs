namespace BackendApartmentReservation.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DbEmployeePlan
    {
        [Key]
        public int Id { get; set; }

        public DbEmployee Employee { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string Description { get; set; }
    }
}
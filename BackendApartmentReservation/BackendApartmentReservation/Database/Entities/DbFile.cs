namespace BackendApartmentReservation.Database.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DbFile
    {
        [Key]
        public int Id { get; set; }

        public byte[] File { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities.Amenities;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    public class DbApartmentReservation
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }
    }
}

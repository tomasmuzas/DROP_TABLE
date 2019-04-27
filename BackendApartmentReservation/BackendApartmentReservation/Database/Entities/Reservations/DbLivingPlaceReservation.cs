using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Database.Entities.Reservations
{
    public class DbLivingPlaceReservation
    {
        [Key]
        public int Id { get; set; }

        public DbApartmentReservation ApartmentReservation { get; set; }

        public DbHotelReservation HotelReservation { get; set; }
    }
}

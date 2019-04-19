using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Database.Entities.Amenities
{
    public class DbHotelAmenity :AmenityBase
    {
        [Key]
        public int Id { get; set; }

        public DbHotelReservation HotelReservation { get; set; }
    }
}

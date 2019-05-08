using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Managers
{
    public class LivingPlaceReservationInfo
    {
        public bool Required { get; set; }

        public ApartmentReservationInfo ApartmentReservationInfo { get; set; }

        public HotelReservationInfo HotelReservationInfo { get; set; }

    }
}

using BackendApartmentReservation.Utilities;
using System;

namespace BackendApartmentReservation.Entities
{
    public class Travel
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public string DestinationOffice { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public TravelOptionStatus TripTickets { get; set; }
        public TravelOptionStatus CarRental { get; set; }
        public TravelOptionStatus Apartments { get; set; }
    }
}

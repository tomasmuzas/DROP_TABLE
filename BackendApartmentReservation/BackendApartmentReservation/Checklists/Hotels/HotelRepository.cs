namespace BackendApartmentReservation.Checklists.Hotels
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HotelRepository : IHotelRepository
    {
        private readonly DatabaseContext _db;

        public HotelRepository(DatabaseContext db)
        {
            _db = db;
        }
    }
}
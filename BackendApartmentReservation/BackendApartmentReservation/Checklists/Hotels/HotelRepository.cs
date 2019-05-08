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

        public async Task<DbHotelAmenity> CreateHotelAmenity(string address)
        {
            var hotelReservation = await _db.HotelReservations.SingleOrDefaultAsync(r => r.Address == address);

            if (hotelReservation == default(DbHotelReservation))
            {
                hotelReservation = new DbHotelReservation
                {
                    Address = address
                };
                await _db.HotelReservations.AddAsync(hotelReservation);
            }

            var hotelAmenity = new DbHotelAmenity
            {
                BookedAt = DateTimeOffset.Now,
                HotelReservation = hotelReservation
            };
            await _db.HotelAmenities.AddAsync(hotelAmenity);

            await _db.SaveChangesAsync();

            return hotelAmenity;
        }

    }
}

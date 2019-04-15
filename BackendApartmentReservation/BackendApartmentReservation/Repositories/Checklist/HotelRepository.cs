using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database;
using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Database.Entities.Reservations;
using BackendApartmentReservation.Repositories.Checklist;
using Microsoft.EntityFrameworkCore;

namespace BackendHotelReservation.Repositories.Checklist
{
    public class HotelRepository : IHotelRepository
    {

        private readonly DatabaseContext _db;

        public HotelRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbHotelAmenity> CreateHotelAmenity(string address)
        {
            var HotelReservation = await _db.HotelReservations.SingleOrDefaultAsync();

            if (HotelReservation == default(DbHotelReservation))
            {
                HotelReservation = new DbHotelReservation
                {
                    Address = address
                };
                await _db.HotelReservations.AddAsync(HotelReservation);
            }

            var hotelAmenity = new DbHotelAmenity
            {
                BookedAt = DateTimeOffset.Now,
                HotelReservation = HotelReservation
            };
            await _db.HotelAmenities.AddAsync(hotelAmenity);

            await _db.SaveChangesAsync();

            return hotelAmenity;


        }

    }
}

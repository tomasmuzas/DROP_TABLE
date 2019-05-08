namespace BackendApartmentReservation.Apartments
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DatabaseContext _db;

        public ApartmentRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbApartmentAmenity> CreateApartmentAmenity(string address)
        {
            var apartmentReservation = await _db.ApartmentReservations.SingleOrDefaultAsync(r => r.Address == address);

            if (apartmentReservation == default(DbApartmentReservation))
            {
                apartmentReservation = new DbApartmentReservation
                {
                    Address = address
                };
                await _db.ApartmentReservations.AddAsync(apartmentReservation);
            }

            var apartmentAmenity = new DbApartmentAmenity
            {
                BookedAt = DateTimeOffset.Now,
                ApartmentReservation = apartmentReservation
            };
            await _db.ApartmentAmenities.AddAsync(apartmentAmenity);

            await _db.SaveChangesAsync();

            return apartmentAmenity;
        }
    }
}
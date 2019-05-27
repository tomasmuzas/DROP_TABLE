namespace BackendApartmentReservation.Checklists.Cars
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CarRentRepository : ICarRentRepository
    {
        private readonly DatabaseContext _db;

        public CarRentRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbCarRentAmenity> CreateEmptyCarRent()
        {
            var carReservation = new DbCarReservation();

            var carRentAmenity = new DbCarRentAmenity
            {
                BookedAt = DateTimeOffset.Now,
                CarReservation = carReservation
            };

            await _db.CarReservations.AddAsync(carReservation);
            await _db.CarRentAmenities.AddAsync(carRentAmenity);

            await _db.SaveChangesAsync();

            return carRentAmenity;
        }

        public async Task UpdateCarRent(DbCarRentAmenity carRent)
        {
            _db.Update(carRent);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCarRent(DbCarRentAmenity carRent)
        {
            _db.CarRentAmenities.Remove(carRent);
            await _db.SaveChangesAsync();

            var carReservation = carRent.CarReservation;
            _db.CarReservations.Remove(carReservation);
            await _db.SaveChangesAsync();
        }

    }
}
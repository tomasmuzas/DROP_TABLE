namespace BackendApartmentReservation.Repositories.Checklist
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Microsoft.EntityFrameworkCore;

    public class CarRentRepository : ICarRentRepository
    {
        private readonly DatabaseContext _db;

        public CarRentRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbCarRentAmenity> CreateCarRentAmenityFromCarNumber(string carNumber)
        {
            var carReservation = await _db.CarReservations.SingleOrDefaultAsync(r => r.CardPlateNumber == carNumber);

            if (carReservation == default(DbCarReservation))
            {
                carReservation = new DbCarReservation
                {
                    CardPlateNumber = carNumber
                };

                await _db.CarReservations.AddAsync(carReservation);
            }

            var carRentAmenity = new DbCarRentAmenity
            {
                BookedAt = DateTimeOffset.Now,
                Car = carReservation
            };

            await _db.CarRentAmenities.AddAsync(carRentAmenity);

            await _db.SaveChangesAsync();

            return carRentAmenity;
        }
    }
}

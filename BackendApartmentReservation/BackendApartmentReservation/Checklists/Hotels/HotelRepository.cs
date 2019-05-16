namespace BackendApartmentReservation.Checklists.Hotels
{
    using Database.Entities.Reservations;
    using Database;
    using Interfaces;
    using System.Threading.Tasks;

    public class HotelRepository : IHotelRepository
    {
        private readonly DatabaseContext _db;

        public HotelRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbHotelReservation> CreateEmptyHotelReservation()
        {
            var hotelReservation = new DbHotelReservation();

            await _db.HotelReservations.AddAsync(hotelReservation);
            await _db.SaveChangesAsync();

            return hotelReservation;
        }

        public async Task UpdateHotelReservation(DbHotelReservation hotelReservation)
        {
            _db.Update(hotelReservation);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteHotelReservation(DbHotelReservation hotelReservation)
        {
            _db.HotelReservations.Remove(hotelReservation);
            await _db.SaveChangesAsync();
        }
    }
}
namespace BackendApartmentReservation.Checklists.Hotels
{
    using Database;
    using Interfaces;

    public class HotelRepository : IHotelRepository
    {
        private readonly DatabaseContext _db;

        public HotelRepository(DatabaseContext db)
        {
            _db = db;
        }
    }
}
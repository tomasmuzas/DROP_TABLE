namespace BackendApartmentReservation.Apartments
{
    using Database;
    using Interfaces;

    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DatabaseContext _db;

        public ApartmentRepository(DatabaseContext db)
        {
            _db = db;
        }
    }
}
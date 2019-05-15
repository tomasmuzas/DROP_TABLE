namespace BackendApartmentReservation.Checklists
{
    using System.Threading.Tasks;
    using Apartments;
    using Database;
    using Database.Entities.Amenities;
    using Hotels;
    using Hotels.Interfaces;

    public class LivingPlaceRepository : ILivingPlaceRepository
    {
        private readonly DatabaseContext _db;
        private readonly ApartmentRepository _apartmentRepository;
        private readonly HotelRepository _hotelRepository;

        public LivingPlaceRepository(
            DatabaseContext db,
            ApartmentRepository apartmentRepository,
            HotelRepository hotelRepository)
        {
            _db = db;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = hotelRepository;
        }

        public async Task<DbLivingPlaceAmenity> CreateEmptyLivingPlaceAmenity()
        {
            var livingPlaceAmenity = new DbLivingPlaceAmenity();

            await _db.LivingPlaceAmenities.AddAsync(livingPlaceAmenity);

            await _db.SaveChangesAsync();

            return livingPlaceAmenity;
        }
    }
}
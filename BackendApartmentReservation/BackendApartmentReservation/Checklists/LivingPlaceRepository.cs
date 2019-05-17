namespace BackendApartmentReservation.Checklists
{
    using System.Threading.Tasks;
    using Apartments.Interfaces;
    using Database;
    using Database.Entities.Amenities;
    using Hotels.Interfaces;

    public class LivingPlaceRepository : ILivingPlaceRepository
    {
        private readonly DatabaseContext _db;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IHotelRepository _hotelRepository;

        public LivingPlaceRepository(
            DatabaseContext db,
            IApartmentRepository apartmentRepository,
            IHotelRepository hotelRepository)
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

        public async Task<DbLivingPlaceAmenity> CreateHotelLivingPlaceAmenity(int hotelReservationId)
        {
            var hotelReservation = await _hotelRepository.GetHotelReservationById(hotelReservationId);
            var livingPlaceAmenity = new DbLivingPlaceAmenity()
            {
                HotelReservation = hotelReservation
            };

            await _db.LivingPlaceAmenities.AddAsync(livingPlaceAmenity);
            await _db.SaveChangesAsync();
            return livingPlaceAmenity;
        }
    }
}
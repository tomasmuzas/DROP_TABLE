namespace BackendApartmentReservation.Checklists.Hotels
{
    using System.Threading.Tasks;
    using Apartments;
    using Database;
    using Database.Entities.Amenities;
    using Database.Entities.Reservations;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class LivingPlaceRepository : ILivingPlaceRepository
    {
        private readonly DatabaseContext _db;
        private readonly ApartmentRepository _apartmentRepository;
        private readonly HotelRepository _hotelRepository;

        public LivingPlaceRepository(DatabaseContext db, ApartmentRepository apartmentRepository, HotelRepository hotelRepository)
        {
            _db = db;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = hotelRepository;
        }

        public async Task<DbLivingPlaceAmenity> CreateLivingPlaceAmenity(DbApartmentAmenity apartmentAmenity, DbHotelAmenity hotelAmenity)
        {
            var apartmentReservationId = apartmentAmenity?.ApartmentReservation?.Id;
            var hotelReservationId = hotelAmenity?.HotelReservation?.Id;
            var livingPlaceReservation = await _db.LivingPlaceReservations.SingleOrDefaultAsync(r =>
                r.ApartmentReservation.Id == apartmentReservationId && r.HotelReservation.Id == hotelReservationId);


            if (livingPlaceReservation == default(DbLivingPlaceReservation))
            {
                livingPlaceReservation = new DbLivingPlaceReservation()
                {
                    ApartmentReservation = apartmentAmenity?.ApartmentReservation,
                    HotelReservation = hotelAmenity?.HotelReservation
                };
                await _db.LivingPlaceReservations.AddAsync(livingPlaceReservation);
            }
            var livingPlaceAmenity = new DbLivingPlaceAmenity
            {
                LivingPlaceReservation = livingPlaceReservation
            };

            await _db.LivingPlaceAmenities.AddAsync(livingPlaceAmenity);

            await _db.SaveChangesAsync();

            return livingPlaceAmenity;
        }

        public async Task<DbApartmentAmenity> CreateApartmentAmenity(string address)
        {
            return await _apartmentRepository.CreateApartmentAmenity(address);

        }

        public async Task<DbHotelAmenity> CreateHotelAmenity(string address)
        {
            return await _hotelRepository.CreateHotelAmenity(address);
        }
    }
}

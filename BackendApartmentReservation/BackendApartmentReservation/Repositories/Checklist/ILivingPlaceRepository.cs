using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities.Amenities;

namespace BackendApartmentReservation.Repositories.Checklist
{
    public interface ILivingPlaceRepository
    {
        Task<DbLivingPlaceAmenity> CreateLivingPlaceAmenity(DbApartmentAmenity apartmentAmenity, DbHotelAmenity hotelAmenity);
        Task<DbApartmentAmenity> CreateApartmentAmenity(string address);
        Task<DbHotelAmenity> CreateHotelAmenity(string address);
    }
}

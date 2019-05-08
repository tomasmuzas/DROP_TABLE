using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities.Amenities;

namespace BackendApartmentReservation.Repositories.Checklist
{
    public interface IHotelRepository
    {
        Task<DbHotelAmenity> CreateHotelAmenity(string address);
    }
}

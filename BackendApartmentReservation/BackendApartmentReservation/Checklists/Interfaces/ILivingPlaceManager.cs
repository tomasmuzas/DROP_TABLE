using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.LivingPlace.Interfaces
{
    public interface ILivingPlaceManager
    {
     Task<int> GetNumberOfAvailableApartmentRooms(DateTimeOffset dateFrom, DateTimeOffset dateTo);
    }
}

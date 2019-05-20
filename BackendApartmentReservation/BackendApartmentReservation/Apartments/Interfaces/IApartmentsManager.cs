using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Apartments.Interfaces
{
    public interface IApartmentsManager
    {
     Task<int> GetNumberOfAvailableApartmentsRooms(string dateFrom, string dateTo);
    }
}

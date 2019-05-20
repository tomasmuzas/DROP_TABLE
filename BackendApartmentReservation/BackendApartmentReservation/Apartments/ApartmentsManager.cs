using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Apartments.Interfaces;

namespace BackendApartmentReservation.Apartments
{
    public class ApartmentsManager : IApartmentsManager
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentsManager(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<int> GetNumberOfAvailableApartmentsRooms(string dateFrom, string dateTo)
        {
            return await _apartmentRepository.GetNumberOfAvailableApartmentsRooms(DateTimeOffset.Parse(dateFrom), DateTimeOffset.Parse(dateTo));
        }
    }
}

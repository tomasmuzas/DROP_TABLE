using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Apartments.Interfaces;
using BackendApartmentReservation.LivingPlace.Interfaces;

namespace BackendApartmentReservation.LivingPlace
{
    public class LivingPlaceManager : ILivingPlaceManager
    {
        private readonly IApartmentRepository _apartmentRepository;

        public LivingPlaceManager(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<int> GetNumberOfAvailableApartmentRooms(string dateFrom, string dateTo)
        {
            return  _apartmentRepository.GetAvailableRooms(DateTimeOffset.Parse(dateFrom), DateTimeOffset.Parse(dateTo)).Result.Count();
        }
    }
}

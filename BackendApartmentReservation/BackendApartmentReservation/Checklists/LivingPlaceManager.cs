using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Apartments.Interfaces;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
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

        public async Task<ApartmentCreatedResponse> CreateApartment(CreateApartmentRequest apartmentRequest)
        {
            var apartment =
                await _apartmentRepository.CreateApartment(apartmentRequest.Address, apartmentRequest.Rooms);

            return new ApartmentCreatedResponse
            {
                ApartmentId = apartment.Id.ToString()
            };
        }

        public async Task<int> GetNumberOfAvailableApartmentRooms(string tripId,DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var availableRooms = await _apartmentRepository.GetAvailableRooms(tripId ,dateFrom, dateTo);
            return availableRooms.Count();
        }
    }
}

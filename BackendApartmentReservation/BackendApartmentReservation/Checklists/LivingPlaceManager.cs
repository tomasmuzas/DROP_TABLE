using BackendApartmentReservation.Apartments.Interfaces;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.LivingPlace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.LivingPlace
{
    public class LivingPlaceManager : ILivingPlaceManager
    {
        private readonly IApartmentRepository _apartmentRepository;

        public LivingPlaceManager(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task CreateApartment(CreateApartmentRequest apartmentRequest)
        {
            var rooms = new List<DbApartmentRoom>();
            foreach (var room in apartmentRequest.Rooms)
            {
                rooms.Add(new DbApartmentRoom { RoomNumber = room.RoomNumber });
            }
            await _apartmentRepository.CreateApartment(apartmentRequest.Address, rooms);
        }

        public async Task<IEnumerable<DbApartment>> GetAllApartments()
        {
            return await _apartmentRepository.GetAllApartments();
        }

        public async Task<DbApartment> GetApartmentsById(int apartmentId)
        {
            return await _apartmentRepository.GetApartmentsById(apartmentId);
        }

        public async Task<int> GetNumberOfAvailableApartmentRooms(string tripId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var availableRooms = await _apartmentRepository.GetAvailableRooms(tripId, dateFrom, dateTo);
            return availableRooms.Count();
        }

        public async Task<DbApartment> UpdateApartmentAddress(int apartmentId, string address)
        {
            var apartment = await _apartmentRepository.GetApartmentsById(apartmentId);
            apartment.Address = address;
            await _apartmentRepository.UpdateApartment(apartment);
            return apartment;
        }
    }
}

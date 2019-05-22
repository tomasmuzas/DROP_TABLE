using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Apartments.Interfaces;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;
using BackendApartmentReservation.LivingPlace.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                rooms.Add(new DbApartmentRoom{RoomNumber = room.RoomNumber});
            }
                await _apartmentRepository.CreateApartment(apartmentRequest.Address, rooms);
        }

        public async Task<int> GetNumberOfAvailableApartmentRooms(string tripId,DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var availableRooms = await _apartmentRepository.GetAvailableRooms(tripId ,dateFrom, dateTo);
            return availableRooms.Count();
        }
    }
}

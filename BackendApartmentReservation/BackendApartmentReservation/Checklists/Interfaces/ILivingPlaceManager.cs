using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Responses;

namespace BackendApartmentReservation.LivingPlace.Interfaces
{
    public interface ILivingPlaceManager
    {
     Task<int> GetNumberOfAvailableApartmentRooms(string tripId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
     Task<ApartmentCreatedResponse> CreateApartment(CreateApartmentRequest apartmentRequest);
    }
}

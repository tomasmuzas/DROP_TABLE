using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendApartmentReservation.LivingPlace.Interfaces
{
    public interface ILivingPlaceManager
    {
        Task<DbApartment> GetApartmentsById(int apartmentId);

        Task<IEnumerable<DbApartment>> GetAllApartments();

        Task<DbApartment> UpdateApartmentAddress(int apartmentId, string address);

        Task<int> GetNumberOfAvailableApartmentRooms(string tripId, DateTimeOffset dateFrom, DateTimeOffset dateTo);

        Task CreateApartment(CreateApartmentRequest apartmentRequest);
    }
}

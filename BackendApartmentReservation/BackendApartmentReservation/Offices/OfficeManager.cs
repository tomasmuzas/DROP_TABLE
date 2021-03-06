﻿namespace BackendApartmentReservation.Offices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Database.Entities;
    using DataContracts.DataTransferObjects.Responses;
    using Interfaces;

    public class OfficeManager : IOfficeManager
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeManager(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<IEnumerable<OfficeInfoResponse>> GetAllOffices()
        {
            var offices = await _officeRepository.GetAllOffices();

            return offices.Select(o => new OfficeInfoResponse
            {
                Id = o.ExternalOfficeId,
                Address = o.Address
            });
        }

        public async Task<DbOffice> GetOfficeById(string officeId)
        {
            return await _officeRepository.GetOfficeById(officeId);
        }
    }
}
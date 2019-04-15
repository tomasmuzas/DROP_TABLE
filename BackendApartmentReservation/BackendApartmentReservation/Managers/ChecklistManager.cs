using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    using Microsoft.Extensions.Logging;
    using Repositories.Checklist;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;
        private readonly ILivingPlaceRepository _livingPlaceRepository;

        private readonly IChecklistRepository _checklistRepository;

        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IFlightRepository flightRepository,
            ICarRentRepository carRentRepository,
            ILivingPlaceRepository livingPlaceRepository,
            IChecklistRepository checklistRepository,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _flightRepository = flightRepository;
            _carRentRepository = carRentRepository;
            _livingPlaceRepository = livingPlaceRepository;

            _checklistRepository = checklistRepository;

            _logger = logger;
        }

        public async Task<DbEmployeeAmenitiesChecklist> CreateChecklistForEmployee(
            int employeeId,
            FlightReservationInfo flightReservationInfo,
            CarReservationInfo carReservationInfo,
            LivingPlaceReservationInfo livingPlaceReservationInfo)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId);

            var checklist = new DbEmployeeAmenitiesChecklist
            {
                Employee = employee
            };

            if (flightReservationInfo.Required)
            {
                var flightAmenity = await _flightRepository.CreateFlightAmenityFromFlightNumber(flightReservationInfo.FlightNumber);
                checklist.Flight = flightAmenity;
                _logger.LogInformation($"Created flight amenity for employee {employee.Id}.");
            }

            if (carReservationInfo.Required)
            {
                var carAmenity = await _carRentRepository.CreateCarRentAmenityFromCarNumber(carReservationInfo.CarNumber);
                checklist.Car = carAmenity;
                _logger.LogInformation($"Created car rent amenity for employee {employee.Id}.");
            }

            if (livingPlaceReservationInfo.Required) { 

            DbApartmentAmenity apartmentAmenity = null;
            DbHotelAmenity hotelAmenity = null;
            if (livingPlaceReservationInfo.ApartmentReservationInfo.Required)
            {
                apartmentAmenity =
                    await _livingPlaceRepository.CreateApartmentAmenity(livingPlaceReservationInfo
                        .ApartmentReservationInfo.ApartmentAddress);
            }

            if (livingPlaceReservationInfo.HotelReservationInfo.Required)
            {
                hotelAmenity =
                    await _livingPlaceRepository.CreateHotelAmenity(livingPlaceReservationInfo
                        .HotelReservationInfo.HotelAddress);
            }




            var livingPlaceAmenity = await _livingPlaceRepository.CreateLivingPlaceAmenity(apartmentAmenity, hotelAmenity);
            checklist.LivingPlace = livingPlaceAmenity;
            _logger.LogInformation($"Created living place amenity for employee {employee.Id}.");
            }

            await _checklistRepository.AddChecklist(checklist);

            return checklist;
        }
    }
}

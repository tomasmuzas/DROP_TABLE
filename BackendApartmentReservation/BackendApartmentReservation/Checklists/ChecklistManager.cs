using BackendApartmentReservation.Checklists.Cars.Interfaces;
using BackendApartmentReservation.Infrastructure.Exceptions;

namespace BackendApartmentReservation.Checklists
{
    using System;
    using System.Threading.Tasks;
    using Hotels.Interfaces;
    using Database.Entities;
    using DataContracts.DataTransferObjects.IntermediaryDTOs;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Employees.Interfaces;
    using Flights.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Logging;
    using Trips.Interfaces;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly ILivingPlaceRepository _livingPlaceRepository;

        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;
        private readonly IHotelRepository _hotelRepository;

        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            ITripRepository tripRepository,
            IFlightRepository flightRepository,
            ICarRentRepository carRentRepository,
            ILivingPlaceRepository livingPlaceRepository,
            IHotelRepository hotelRepository,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _tripRepository = tripRepository;
            _checklistRepository = checklistRepository;
            _livingPlaceRepository = livingPlaceRepository;

            _flightRepository = flightRepository;
            _carRentRepository = carRentRepository;
            _hotelRepository = hotelRepository;

            _logger = logger;
        }

        public async Task<ChecklistInformationResponse> GetFullChecklistInformation(string employeeId, string tripId)
        {
            var flightInfo = await GetFlightInfo(employeeId, tripId);
            var carInfo = await GetCarRentInfo(employeeId, tripId);

            return new ChecklistInformationResponse
            {
                Flight = flightInfo,
                Car = carInfo
            };
        }

        public async Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(
            string employeeId,
            string tripId)
        {
            var existingChecklist = await _checklistRepository.GetChecklist(employeeId, tripId);

            if (existingChecklist != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already exists.");
                throw new ErrorCodeException(ErrorCodes.ChecklistAlreadyExists);
            }

            var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeId);
            var trip = await _tripRepository.GetTrip(tripId);

            var checklist = new DbEmployeeAmenitiesChecklist
            {
                Employee = employee,
                Trip = trip
            };

            await _checklistRepository.AddChecklist(checklist);
            _logger.LogInformation($"Successfully created empty checklist for trip {tripId}, employee {employeeId}");

            return checklist;
        }

        public async Task AddFlightForEmployee(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            if (checklist.Flight != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already has a flight.");
                throw new ErrorCodeException(ErrorCodes.ChecklistFlightAlreadyExists);
            }

            var flight = await _flightRepository.CreateEmptyFlight();
            checklist.Flight = flight;
            await _checklistRepository.UpdateChecklist(checklist);

            _logger.LogInformation($"Added empty flight to the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task UpdateFlightForEmployee(string employeeId, string tripId, FlightReservationRequest info)
        {
            var flight = await _checklistRepository.GetChecklistFullFlight(employeeId, tripId);

            flight.FlightReservation.FlightNumber = info.FlightNumber;
            flight.FlightReservation.Company = info.Company;
            flight.FlightReservation.AirportAddress = info.AirportAddress;
            flight.FlightReservation.FlightTime = info.FlightTime;

            await _flightRepository.UpdateFlight(flight);
            _logger.LogInformation(
                $"Updated flight information for the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task<FlightReservationInfo> GetFlightInfo(string employeeId, string tripId)
        {
            var flight = await _checklistRepository.GetChecklistFullFlight(employeeId, tripId);

            var flightInfo = new FlightReservationInfo();

            if (flight == null)
            {
                flightInfo.IsRequired = false;
                return flightInfo;
            }

            flightInfo.IsRequired = true;
            flightInfo.FlightNumber = flight.FlightReservation.FlightNumber;
            flightInfo.Company = flight.FlightReservation.Company;
            flightInfo.AirportAddress = flight.FlightReservation.AirportAddress;
            flightInfo.FlightTime = flight.FlightReservation.FlightTime;

            return flightInfo;
        }

        public async Task DeleteFlight(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            var flight = checklist.Flight;

            checklist.Flight = null;
            await _checklistRepository.UpdateChecklist(checklist);

            await _flightRepository.DeleteFlight(flight);
            _logger.LogInformation($"Deleted flight for the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task AddCarRentForEmployee(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);

            if (checklist.Car != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already has a car rent.");
                throw new ErrorCodeException(ErrorCodes.ChecklistCarAlreadyExists);
            }

            var carRent = await _carRentRepository.CreateEmptyCarRent();
            checklist.Car = carRent;
            await _checklistRepository.UpdateChecklist(checklist);

            _logger.LogInformation($"Added empty car rent to the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task UpdateCarRentForEmployee(string employeeId, string tripId, CarReservationRequest info)
        {
            var carRent = await _checklistRepository.GetChecklistFullCarRent(employeeId, tripId);

            carRent.CarReservation.CarNumber = info.CarNumber;
            carRent.CarReservation.RentStartTime = info.RentStartTime;
            carRent.CarReservation.RentEndTime = info.RentEndTime;
            carRent.CarReservation.CarAddress = info.CarAddress;

            await _carRentRepository.UpdateCarRent(carRent);
            _logger.LogInformation($"Updated car rent information for the checklist for employee {employeeId} and trip {tripId}");

        }

        public async Task<CarReservationInfo> GetCarRentInfo(string employeeId, string tripId)
        {
            var carRent = await _checklistRepository.GetChecklistFullCarRent(employeeId, tripId);

            var carReservationInfo = new CarReservationInfo();

            if (carRent == null)
            {
                carReservationInfo.IsRequired = false;
                return carReservationInfo;
            }

            carReservationInfo.IsRequired = true;
            carReservationInfo.CarNumber = carRent.CarReservation.CarNumber;
            carReservationInfo.RentStartTime = carRent.CarReservation.RentStartTime;
            carReservationInfo.RentEndTime = carRent.CarReservation.RentEndTime;
            carReservationInfo.CarAddress = carRent.CarReservation.CarAddress;

            return carReservationInfo;
        }

        public async Task DeleteCarRent(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            var carRent = checklist.Car;

            checklist.Car = null;
            await _checklistRepository.UpdateChecklist(checklist);

            await _carRentRepository.DeleteCarRent(carRent);
            _logger.LogInformation($"Deleted car rent for the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task AddHotelReservationForEmployee(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            if (checklist.LivingPlace != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already has a living place");
                throw new ErrorCodeException(ErrorCodes.ChecklistLivingPlaceAlreadyExists);
            }

            var hotelReservation = await _hotelRepository.CreateEmptyHotelReservation();
            var livingPlace = await _livingPlaceRepository.CreateHotelLivingPlaceAmenity(hotelReservation.Id);
            checklist.LivingPlace = livingPlace;

            await _checklistRepository.UpdateChecklist(checklist);
            _logger.LogInformation($"Added empty hotel reservation as living place to the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task DeleteHotelReservation(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            var hotelReservation = checklist.LivingPlace.HotelReservation;

            checklist.LivingPlace = null;
            await _checklistRepository.UpdateChecklist(checklist);

            await _hotelRepository.DeleteHotelReservation(hotelReservation);
            _logger.LogInformation($"Deleted hotel reservation as living place for the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task<HotelReservationInfo> GetHotelReservationInfo(string employeeId, string tripId)
        {
            var hotelReservation = await _checklistRepository.GetChecklistFullHotelReservation(employeeId, tripId);

            var hotelReservationInfo = new HotelReservationInfo();

            if (hotelReservation == null)
            {
                hotelReservationInfo.Required = false;
                return hotelReservationInfo;
            }

            hotelReservationInfo.Required = true;
            hotelReservationInfo.HotelName = hotelReservation.HotelName;
            hotelReservationInfo.DateFrom = hotelReservation.DateFrom;
            hotelReservationInfo.DateTo = hotelReservation.DateTo;

            return hotelReservationInfo;
        }

        public async Task UpdateHotelReservationForEmployee(string employeeId, string tripId, HotelReservationRequest info)
        {
            var hotelReservation = await _checklistRepository.GetChecklistFullHotelReservation(employeeId, tripId);

            hotelReservation.HotelName = info.HotelName;
            hotelReservation.DateTo = info.DateTo;
            hotelReservation.DateFrom = info.DateFrom;

            await _hotelRepository.UpdateHotelReservation(hotelReservation);
            _logger.LogInformation($"Updated hotel reservation information for the checklist for employee {employeeId} and trip {tripId}");
        }
    }
}
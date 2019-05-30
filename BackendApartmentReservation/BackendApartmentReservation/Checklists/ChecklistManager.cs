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
    using System.Collections.Generic;
    using BackendApartmentReservation.Apartments.Interfaces;
    using BackendApartmentReservation.Groups.Interfaces;
    using BackendApartmentReservation.Confirmations.Interfaces;
    using Infrastructure.Utilities;
    using Microsoft.AspNetCore.Http;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly ILivingPlaceRepository _livingPlaceRepository;

        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IConfirmationRepository _confirmationRepository;

        private readonly IFileManager _fileManager;
        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            ITripRepository tripRepository,
            IGroupRepository groupRepository,
            IFlightRepository flightRepository,
            ICarRentRepository carRentRepository,
            ILivingPlaceRepository livingPlaceRepository,
            IHotelRepository hotelRepository,
            IApartmentRepository apartmentRepository,
            IConfirmationRepository confirmationRepository,
            IFileManager fileManager,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _tripRepository = tripRepository;
            _groupRepository = groupRepository;
            _checklistRepository = checklistRepository;
            _livingPlaceRepository = livingPlaceRepository;

            _flightRepository = flightRepository;
            _carRentRepository = carRentRepository;
            _hotelRepository = hotelRepository;
            _apartmentRepository = apartmentRepository;
            _confirmationRepository = confirmationRepository;

            _fileManager = fileManager;
            _logger = logger;
        }

        public async Task<ChecklistInformationResponse> GetFullChecklistInformation(string employeeId, string tripId)
        {
            var flightInfo = await GetFlightInfo(employeeId, tripId);
            var carInfo = await GetCarRentInfo(employeeId, tripId);
            var hotelReservationInfo = await GetHotelReservationInfo(employeeId, tripId);
            var apartmentReservationInfo = await GetApartmentReservationInfo(employeeId, tripId);

            return new ChecklistInformationResponse
            {
                TripId = tripId,
                Flight = flightInfo,
                Car = carInfo,
                LivingPlace = new LivingPlaceReservationInfo
                {
                    IsRequired = hotelReservationInfo.Required || apartmentReservationInfo.Required,
                    ApartmentReservationInfo = apartmentReservationInfo,
                    HotelReservationInfo = hotelReservationInfo
                }
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

        public async Task UpdateFlightTicketForEmployee(string employeeId, string tripId, IFormFile file)
        {
            var flight = await _checklistRepository.GetChecklistFullFlight(employeeId, tripId);

            var newFile = await _fileManager.UploadFile(file);
            if (flight.Ticket != null)
            {
                await _fileManager.DeleteFile(flight.Ticket);
            }

            flight.Ticket = newFile;

            await _flightRepository.UpdateFlight(flight);
            _logger.LogInformation(
                $"Updated ticket for the flight for employee {employeeId} and trip {tripId}");
        }

        public async Task UpdateCarRentDocumentForEmployee(string employeeId, string tripId, IFormFile file)
        {
            var car = await _checklistRepository.GetChecklistFullCarRent(employeeId, tripId);

            var newFile = await _fileManager.UploadFile(file);
            if (car.Documents != null)
            {
                await _fileManager.DeleteFile(car.Documents);
            }

            car.Documents = newFile;

            await _carRentRepository.UpdateCarRent(car);
            _logger.LogInformation(
                $"Updated documents for the car rent for employee {employeeId} and trip {tripId}");
        }

        public async Task DeleteLivingPlace(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            var hotel = checklist.LivingPlace?.HotelReservation;

            if (hotel != null)
            {
                await _hotelRepository.DeleteHotelReservation(hotel);
                _logger.LogInformation($"Deleted hotel reservation for employee {employeeId} and trip {tripId}");
            }

            var apartmentReservation = checklist.LivingPlace?.ApartmentRoomReservation;

            if (apartmentReservation != null)
            {
                await _apartmentRepository.DeleteRoomReservation(apartmentReservation);
                _logger.LogInformation($"Deleted apartment room for employee {employeeId} and trip {tripId}");
            }

            checklist.LivingPlace = null;
            await _checklistRepository.UpdateChecklist(checklist);
            _logger.LogInformation($"Deleted living place information for employee {employeeId} and trip {tripId}");
        }

        public async Task UpdateHotelDocumentsForEmployee(string employeeId, string tripId, IFormFile file)
        {
            var hotel = await _checklistRepository.GetChecklistFullHotelReservation(employeeId, tripId);

            var newFile = await _fileManager.UploadFile(file);
            if (hotel.Documents != null)
            {
                await _fileManager.DeleteFile(hotel.Documents);
            }

            hotel.Documents = newFile;

            await _hotelRepository.UpdateHotelReservation(hotel);
            _logger.LogInformation(
                $"Updated documents for the hotel for employee {employeeId} and trip {tripId}");
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
            flightInfo.FlightTime = DateTimeFormatter.GetStandardDateTimeString(flight.FlightReservation.FlightTime);
            flightInfo.TicketFileId = flight.Ticket?.ExternalFileId;

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
            carReservationInfo.RentStartTime = DateTimeFormatter.GetStandardDateTimeString(carRent.CarReservation.RentStartTime);
            carReservationInfo.RentEndTime = DateTimeFormatter.GetStandardDateTimeString(carRent.CarReservation.RentEndTime);
            carReservationInfo.CarAddress = carRent.CarReservation.CarAddress;
            carReservationInfo.DocumentsFileId = carRent.Documents?.ExternalFileId;

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
            hotelReservationInfo.DateFrom = DateTimeFormatter.GetStandardDateTimeString(hotelReservation.DateFrom);
            hotelReservationInfo.DateTo = DateTimeFormatter.GetStandardDateTimeString(hotelReservation.DateTo);
            hotelReservationInfo.DocumentsFileId = hotelReservation.Documents?.ExternalFileId;

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

        public async Task AddApartmentReservationForEmployee(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            if (checklist.LivingPlace != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already has a living place");
                throw new ErrorCodeException(ErrorCodes.ChecklistLivingPlaceAlreadyExists);
            }

            var apartmentReservation = await _apartmentRepository.CreateRoomReservation(checklist.Trip.ExternalTripId, 
                checklist.Employee, checklist.Trip.DepartureDate, checklist.Trip.ReturnDate);
            var livingPlace = await _livingPlaceRepository.CreateApartmentLivingPlaceAmenity(apartmentReservation.Id);
            checklist.LivingPlace = livingPlace;

            await _checklistRepository.UpdateChecklist(checklist);
            _logger.LogInformation($"Added apartment room reservation as living place to the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task AddApartmentReservationForAllEmployees(string tripId)
        {
            var trip = await _tripRepository.GetTrip(tripId);
            foreach (var group in trip.Groups)
            {
                var employeesGroup = await _groupRepository.GetEmployeeGroupsByGroupId(group.ExternalGroupId);
                foreach (var employeeGroup in employeesGroup)
                {
                    await AddApartmentReservationForEmployee(employeeGroup.DbEmployee.ExternalEmployeeId, trip.ExternalTripId);
                }
            }
        }

        public async Task DeleteApartmentReservation(string employeeId, string tripId)
        {
            var checklist = await _checklistRepository.GetFullChecklist(employeeId, tripId);
            var apartmentReservation = checklist.LivingPlace.ApartmentRoomReservation;

            checklist.LivingPlace = null;
            await _checklistRepository.UpdateChecklist(checklist);

            await _apartmentRepository.DeleteRoomReservation(apartmentReservation);
            _logger.LogInformation($"Deleted apartment room reservation as living place for the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task<ApartmentReservationInfo> GetApartmentReservationInfo(string employeeId, string tripId)
        {
            var trip = await _tripRepository.GetTrip(tripId);
            var apartmentReservation = await _checklistRepository.GetChecklistFullApartmentRoomReservation(employeeId, tripId);

            var apartmentReservationInfo = new ApartmentReservationInfo();

            if (apartmentReservation == null)
            {
                apartmentReservationInfo.Required = false;
                return apartmentReservationInfo;
            }

            apartmentReservationInfo.Required = true;
            apartmentReservationInfo.ApartmentAddress = trip.DestinationOffice.OfficeApartment.Address;
            apartmentReservationInfo.RoomNumber = apartmentReservation.Room.RoomNumber;
            apartmentReservationInfo.DateFrom = DateTimeFormatter.GetStandardDateTimeString(apartmentReservation.DateFrom);
            apartmentReservationInfo.DateTo = DateTimeFormatter.GetStandardDateTimeString(apartmentReservation.DateTo);

            return apartmentReservationInfo;
        }

        public async Task UpdateApartmentReservationForEmployee(string employeeId, string tripId, ApartmentReservationRequest info)
        {
            var apartmentReservation = await _checklistRepository.GetChecklistFullApartmentRoomReservation(employeeId, tripId);

            apartmentReservation.Room = info.Room;
            apartmentReservation.DateTo = info.DateTo;
            apartmentReservation.DateFrom = info.DateFrom;

            await _apartmentRepository.UpdateRoomReservation(apartmentReservation);
            _logger.LogInformation($"Updated hotel reservation information for the checklist for employee {employeeId} and trip {tripId}");
        }

        public async Task<IEnumerable<DbEmployeeAmenitiesChecklist>> GetAllTripChecklists(string tripId)
        {
            return await _checklistRepository.GetAllTripChecklists(tripId);
        }

        public async Task UpdateChecklistTrip(DbEmployeeAmenitiesChecklist checklist, string newTripId)
        {
            var trip = await _tripRepository.GetTrip(newTripId);
            checklist.Trip = trip;
            await _checklistRepository.UpdateChecklist(checklist);
        }

        public async Task AcceptTripParticipationConfirmation(string employeeId, string tripId)
        {
            var confirmation = await _confirmationRepository.GetWaitingConfirmation(ConfirmationType.TripParticipation,
                employeeId, tripId);

            if (confirmation == null)
            {
                _logger.LogError($"Waiting trip participation confirmation for employee {employeeId} and trip {tripId} was not found");
                throw new ErrorCodeException(ErrorCodes.WaitingConfirmationNotFound);
            }

            await _confirmationRepository.AcceptConfirmation(confirmation);
        }

        public async Task DeclineTripParticipationConfirmation(string employeeId, string tripId)
        {
            var confirmation = await _confirmationRepository.GetWaitingConfirmation(ConfirmationType.TripParticipation,
                employeeId, tripId);

            if (confirmation == null)
            {
                _logger.LogError($"Waiting trip participation confirmation for employee {employeeId} and trip {tripId} was not found");
                throw new ErrorCodeException(ErrorCodes.WaitingConfirmationNotFound);
            }

            await _confirmationRepository.DeclineConfirmation(confirmation);
        }

        public async Task AcceptTripMergeConfirmation(string employeeId, string tripId)
        {
            var confirmation = await _confirmationRepository.GetWaitingConfirmation(ConfirmationType.TripMerge,
                employeeId, tripId);

            if (confirmation == null)
            {
                _logger.LogError($"Waiting trip merge confirmation for employee {employeeId} and trip {tripId} was not found");
                throw new ErrorCodeException(ErrorCodes.WaitingConfirmationNotFound);
            }

            await _confirmationRepository.AcceptConfirmation(confirmation);
        }

        public async Task DeclineTripMergeConfirmation(string employeeId, string tripId)
        {
            var confirmation = await _confirmationRepository.GetWaitingConfirmation(ConfirmationType.TripMerge,
                employeeId, tripId);

            if (confirmation == null)
            {
                _logger.LogError($"Waiting trip merge confirmation for employee {employeeId} and trip {tripId} was not found");
                throw new ErrorCodeException(ErrorCodes.WaitingConfirmationNotFound);
            }

            await _confirmationRepository.DeclineConfirmation(confirmation);
        }
    }
}
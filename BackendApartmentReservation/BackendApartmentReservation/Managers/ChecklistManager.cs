using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    using System;
    using DataContracts.DataTransferObjects.Requests;
    using DataContracts.DataTransferObjects.Responses;
    using Microsoft.Extensions.Logging;
    using Repositories.Checklist;

    public class ChecklistManager : IChecklistManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;

        private readonly IFlightRepository _flightRepository;

        private readonly ILogger<ChecklistManager> _logger;

        public ChecklistManager(
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            ITripRepository tripRepository,
            IFlightRepository flightRepository,
            ILogger<ChecklistManager> logger)
        {
            _employeeRepository = employeeRepository;
            _tripRepository = tripRepository;
            _checklistRepository = checklistRepository;

            _flightRepository = flightRepository;

            _logger = logger;
        }

        public async Task<ChecklistInformationResponse> GetFullChecklistInformation(string employeeId, string tripId)
        {
            var flightInfo = await GetFlightInfo(employeeId, tripId);

            return new ChecklistInformationResponse
            {
                Flight = flightInfo
            };
        }

        public async Task<DbEmployeeAmenitiesChecklist> CreateEmptyChecklistForEmployee(string employeeId, string tripId)
        {
            var existingChecklist = await _checklistRepository.GetChecklist(employeeId, tripId);

            if (existingChecklist != null)
            {
                _logger.LogError($"Checklist for employee {employeeId} and trip {tripId} already exists.");
                throw new ArgumentException("");
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
                throw new ArgumentException("");
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
            _logger.LogInformation($"Updated flight information for the checklist for employee {employeeId} and trip {tripId}");
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
    }
}

using System;
using BackendApartmentReservation.Checklists.Cars.Interfaces;
using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Database.Entities.Reservations;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Infrastructure.Exceptions;

namespace BackendApartmentReservation.Tests.Managers
{
    using System.Threading.Tasks;
    using Checklists;
    using Checklists.Flights.Interfaces;
    using Checklists.Interfaces;
    using Database.Entities;
    using Employees.Interfaces;
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Trips.Interfaces;
    using Xunit;

    public class ChecklistManagerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;

        private readonly ChecklistManager _manager;

        public ChecklistManagerTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _tripRepository = A.Fake<ITripRepository>();
            _flightRepository = A.Fake<IFlightRepository>();
            _carRentRepository = A.Fake<ICarRentRepository>();

            _checklistRepository = A.Fake<IChecklistRepository>(o => o.Strict());

            ILogger<ChecklistManager> logger = new NullLogger<ChecklistManager>();

            _manager = new ChecklistManager(
                _employeeRepository,
                _checklistRepository,
                _tripRepository,
                _flightRepository,
                _carRentRepository,
                logger);
        }

        [Fact]
        public async Task CreateChecklistForEmployee_CreatesProperChecklist()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var employeeRepositoryCall = A.CallTo(() => _employeeRepository.GetEmployeeByEmployeeId(employee.ExternalEmployeeId));

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var tripRepositoryCall = A.CallTo(() => _tripRepository.GetTrip(trip.ExternalTripId));

            var checklistRepositoryCall =
                A.CallTo(() => _checklistRepository.AddChecklist(A<DbEmployeeAmenitiesChecklist>._));

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult((DbEmployeeAmenitiesChecklist)null));

            employeeRepositoryCall.Returns(employee);
            tripRepositoryCall.Returns(trip);
            checklistRepositoryCall.Returns(Task.CompletedTask);

            // TODO: @tomu figure out logging tests
            //var carLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created car rent amenity for employee {employee.Id}."));

            //var flightLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created flight amenity for employee {employee.Id}."));

            var checklist = await _manager.CreateEmptyChecklistForEmployee(
                employee.ExternalEmployeeId,
                trip.ExternalTripId);

            employeeRepositoryCall.MustHaveHappenedOnceExactly();
            tripRepositoryCall.MustHaveHappenedOnceExactly();
            checklistRepositoryCall.MustHaveHappenedOnceExactly();
            getChecklistCall.MustHaveHappenedOnceExactly();

            Assert.Equal(checklist.Employee, employee);
            Assert.Equal(checklist.Trip, trip);
        }

        [Fact]
        public async Task CreateChecklistForEmployee_ThrowsIfChecklistExists()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist()));

            // TODO: @tomu figure out logging tests
            //var carLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created car rent amenity for employee {employee.Id}."));

            //var flightLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created flight amenity for employee {employee.Id}."));

            var exception = await Assert.ThrowsAsync<ErrorCodeException>(async () =>
            {
                await _manager.CreateEmptyChecklistForEmployee(
                    employee.ExternalEmployeeId,
                    trip.ExternalTripId);
            });

            Assert.Equal(ErrorCodes.ChecklistAlreadyExists, exception.ErrorCode);

            getChecklistCall.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddFlightForEmployee_CreatesFlight()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetFullChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist
            {
                Flight = null
            }));

            var callToCreateFlight = A.CallTo(() => _flightRepository.CreateEmptyFlight());

            callToCreateFlight.Returns(new DbFlightAmenity
            {
                FlightReservation = new DbFlightReservation()
            });

            var callToUpdateChecklist =
                A.CallTo(() => _checklistRepository.UpdateChecklist(A<DbEmployeeAmenitiesChecklist>._));

            callToUpdateChecklist.Returns(Task.CompletedTask);

            await _manager.AddFlightForEmployee(employee.ExternalEmployeeId, trip.ExternalTripId);

            getChecklistCall.MustHaveHappenedOnceExactly();
            callToCreateFlight.MustHaveHappenedOnceExactly();
            callToUpdateChecklist.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddFlightForEmployee_ThrowsIfFlightExists()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetFullChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist
            {
                Flight = new DbFlightAmenity()
            }));

            // TODO: @tomu figure out logging tests
            //var carLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created car rent amenity for employee {employee.Id}."));

            //var flightLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created flight amenity for employee {employee.Id}."));

            var exception = await Assert.ThrowsAsync<ErrorCodeException>(async () =>
            {
                await _manager.AddFlightForEmployee(
                    employee.ExternalEmployeeId,
                    trip.ExternalTripId);
            });

            Assert.Equal(ErrorCodes.ChecklistFlightAlreadyExists, exception.ErrorCode);

            getChecklistCall.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateFlightForEmployee_UpdatesFlight()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getFlightCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullFlight(employee.ExternalEmployeeId, trip.ExternalTripId));

            getFlightCall.Returns(Task.FromResult(new DbFlightAmenity
            {
                FlightReservation = new DbFlightReservation()
            }));

            var flightInfo = new FlightReservationRequest
            {
                AirportAddress = "new airport address",
                Company = "new company",
                FlightNumber = "new flight number",
                FlightTime = new DateTime()
            };

            var callToUpdateFlight = A.CallTo(() => _flightRepository.UpdateFlight(A<DbFlightAmenity>.That.Matches(f => 
                f.FlightReservation.AirportAddress == flightInfo.AirportAddress
                && f.FlightReservation.Company == flightInfo.Company
                && f.FlightReservation.FlightNumber == flightInfo.FlightNumber
                && f.FlightReservation.FlightTime != null)));

            callToUpdateFlight.Returns(Task.CompletedTask);

            await _manager.UpdateFlightForEmployee(employee.ExternalEmployeeId, trip.ExternalTripId, flightInfo);

            getFlightCall.MustHaveHappenedOnceExactly();
            callToUpdateFlight.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetFlightInfo_MapsCorrectly()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getFlightCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullFlight(employee.ExternalEmployeeId, trip.ExternalTripId));

            getFlightCall.Returns(Task.FromResult(new DbFlightAmenity
            {
                FlightReservation = new DbFlightReservation
                {
                    AirportAddress = "new airport address",
                    Company = "new company",
                    FlightNumber = "new flight number",
                    FlightTime = new DateTime()
                }
            }));

            var info = await _manager.GetFlightInfo(employee.ExternalEmployeeId, trip.ExternalTripId);

            getFlightCall.MustHaveHappenedOnceExactly();

            Assert.True(info.IsRequired);
            Assert.Equal("new airport address", info.AirportAddress);
            Assert.Equal("new company", info.Company);
            Assert.Equal("new flight number", info.FlightNumber);
            Assert.NotNull(info.FlightTime);
        }

        [Fact]
        public async Task DeleteFlightForEmployee_DeletesFlight()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetFullChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist
            {
                Flight = new DbFlightAmenity
                {
                    Id = 4
                }
            }));

            var callToUpdateChecklist = A.CallTo(() => _checklistRepository.UpdateChecklist(A<DbEmployeeAmenitiesChecklist>.That.Matches(c =>
                c.Flight == null)));

            callToUpdateChecklist.Returns(Task.CompletedTask);

            var callToDeleteFlight = A.CallTo(() => _flightRepository.DeleteFlight(A<DbFlightAmenity>._));

            callToDeleteFlight.Returns(Task.CompletedTask);

            await _manager.DeleteFlight(employee.ExternalEmployeeId, trip.ExternalTripId);

            getChecklistCall.MustHaveHappenedOnceExactly();
            callToUpdateChecklist.MustHaveHappenedOnceExactly();
            callToDeleteFlight.MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task GetFullChecklistInformation_MapsCorrectly()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getFlightCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullFlight(employee.ExternalEmployeeId, trip.ExternalTripId));

            getFlightCall.Returns(Task.FromResult(new DbFlightAmenity
            {
                FlightReservation = new DbFlightReservation
                {
                    AirportAddress = "new airport address",
                    Company = "new company",
                    FlightNumber = "new flight number",
                    FlightTime = new DateTime()
                }
            }));

            var info = await _manager.GetFullChecklistInformation(employee.ExternalEmployeeId, trip.ExternalTripId);

            getFlightCall.MustHaveHappenedOnceExactly();

            Assert.True(info.Flight.IsRequired);
            Assert.Equal("new airport address", info.Flight.AirportAddress);
            Assert.Equal("new company", info.Flight.Company);
            Assert.Equal("new flight number", info.Flight.FlightNumber);
            Assert.NotNull(info.Flight.FlightTime);
        }
        [Fact]
        public async Task AddCarRentForEmployee_CreatesCarRent()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetFullChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist
            {
                Car = null
            }));

            var callToCreateCarRent = A.CallTo(() => _carRentRepository.CreateEmptyCarRent());

            callToCreateCarRent.Returns(new DbCarRentAmenity()
            {
                CarReservation = new DbCarReservation()
            });

            var callToUpdateChecklist =
                A.CallTo(() => _checklistRepository.UpdateChecklist(A<DbEmployeeAmenitiesChecklist>._));

            callToUpdateChecklist.Returns(Task.CompletedTask);

            await _manager.AddCarRentForEmployee(employee.ExternalEmployeeId, trip.ExternalTripId);

            getChecklistCall.MustHaveHappenedOnceExactly();
            callToCreateCarRent.MustHaveHappenedOnceExactly();
            callToUpdateChecklist.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddCarRentForEmployee_ThrowsIfCarRentExists()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetFullChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist
            {
                Car = new DbCarRentAmenity()
            }));

            var exception = await Assert.ThrowsAsync<ErrorCodeException>(async () =>
            {
                await _manager.AddCarRentForEmployee(
                    employee.ExternalEmployeeId,
                    trip.ExternalTripId);
            });

            Assert.Equal(ErrorCodes.ChecklistCarAlreadyExists, exception.ErrorCode);

            getChecklistCall.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateCarRentForEmployee_UpdatesCarRent()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getCarRentCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullCarRent(employee.ExternalEmployeeId, trip.ExternalTripId));

            getCarRentCall.Returns(Task.FromResult(new DbCarRentAmenity
            {
               CarReservation = new DbCarReservation()
            }));

            var carReservationInfo = new CarReservationRequest()
            {
                CarNumber = "12345",
                CarAddress = "Vilnius",
                RentStartTime = new DateTime(),
                RentEndTime = new DateTime()
            };

            var callToUpdateCarRent = A.CallTo(() => _carRentRepository.UpdateCarRent(A<DbCarRentAmenity>.That.Matches(c =>
                c.CarReservation.CarNumber == carReservationInfo.CarNumber
                && c.CarReservation.CarAddress == carReservationInfo.CarAddress
                && c.CarReservation.RentStartTime != null
                && c.CarReservation.RentEndTime != null)));

            callToUpdateCarRent.Returns(Task.CompletedTask);

            await _manager.UpdateCarRentForEmployee(employee.ExternalEmployeeId, trip.ExternalTripId, carReservationInfo);

            getCarRentCall.MustHaveHappenedOnceExactly();
            callToUpdateCarRent.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetCarRentInfo_MapsCorrectly()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getCarRentCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullCarRent(employee.ExternalEmployeeId, trip.ExternalTripId));

            getCarRentCall.Returns(Task.FromResult(new DbCarRentAmenity()
            {
                CarReservation = new DbCarReservation()
                {
                    CarNumber = "12345",
                    CarAddress = "Vilnius",
                    RentStartTime = new DateTime(),
                    RentEndTime = new DateTime()
                }
            }));

            var info = await _manager.GetCarRentInfo(employee.ExternalEmployeeId, trip.ExternalTripId);

            getCarRentCall.MustHaveHappenedOnceExactly();

            Assert.True(info.IsRequired);
            Assert.Equal("12345", info.CarNumber);
            Assert.Equal("Vilnius", info.CarAddress);
            Assert.NotNull(info.RentStartTime);
            Assert.NotNull(info.RentEndTime);
        }

        [Fact]
        public async Task DeleteCarRentForEmployee_DeletesCarRent()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getChecklistCall = A.CallTo(() =>
                _checklistRepository.GetFullChecklist(employee.ExternalEmployeeId, trip.ExternalTripId));

            getChecklistCall.Returns(Task.FromResult(new DbEmployeeAmenitiesChecklist
            {
                Car = new DbCarRentAmenity()
                {
                    Id = 45
                }
            }));

            var callToUpdateChecklist = A.CallTo(() => _checklistRepository.UpdateChecklist(A<DbEmployeeAmenitiesChecklist>.That.Matches(c =>
                c.Car == null)));

            callToUpdateChecklist.Returns(Task.CompletedTask);

            var callToDeleteCarRent = A.CallTo(() => _carRentRepository.DeleteCarRent(A<DbCarRentAmenity>._));

            callToDeleteCarRent.Returns(Task.CompletedTask);

            await _manager.DeleteCarRent(employee.ExternalEmployeeId, trip.ExternalTripId);

            getChecklistCall.MustHaveHappenedOnceExactly();
            callToUpdateChecklist.MustHaveHappenedOnceExactly();
            callToDeleteCarRent.MustHaveHappenedOnceExactly();
        }

    }
}

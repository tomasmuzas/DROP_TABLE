﻿using System;
using BackendApartmentReservation.Checklists.Cars.Interfaces;
using BackendApartmentReservation.Database.Entities.Amenities;
using BackendApartmentReservation.Database.Entities.Reservations;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using BackendApartmentReservation.Infrastructure.Exceptions;

namespace BackendApartmentReservation.Tests.Managers
{
    using System.Threading.Tasks;
    using Checklists.Hotels.Interfaces;
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
    using Apartments.Interfaces;
    using Groups.Interfaces;
    using Confirmations.Interfaces;

    public class ChecklistManagerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;
        private readonly ILivingPlaceRepository _livingPlaceRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IConfirmationRepository _confirmationRepository;
        private readonly IFileManager _fileManager;

        private readonly ChecklistManager _manager;

        public ChecklistManagerTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _tripRepository = A.Fake<ITripRepository>();
            _groupRepository = A.Fake<IGroupRepository>();
            _flightRepository = A.Fake<IFlightRepository>();
            _carRentRepository = A.Fake<ICarRentRepository>();
            _livingPlaceRepository = A.Fake<ILivingPlaceRepository>();
            _hotelRepository = A.Fake<IHotelRepository>();
            _apartmentRepository = A.Fake<IApartmentRepository>();
            _confirmationRepository = A.Fake<IConfirmationRepository>();
            _fileManager = A.Fake<IFileManager>();

            _checklistRepository = A.Fake<IChecklistRepository>(o => o.Strict());

            ILogger<ChecklistManager> logger = new NullLogger<ChecklistManager>();

            _manager = new ChecklistManager(
                _employeeRepository,
                _checklistRepository,
                _tripRepository,
                _groupRepository,
                _flightRepository,
                _carRentRepository,
                _livingPlaceRepository,
                _hotelRepository,
                _apartmentRepository,
                _confirmationRepository,
                _fileManager,
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
                FlightTime = new DateTimeOffset()
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
                    FlightTime = new DateTimeOffset()
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
                    FlightTime = new DateTimeOffset()
                }
            }));

            var getCarCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullCarRent(employee.ExternalEmployeeId, trip.ExternalTripId));

            getCarCall.Returns(Task.FromResult(new DbCarRentAmenity
            {
                CarReservation = new DbCarReservation
                {
                    CarAddress = "new car address",
                    CarNumber = "CAR123",
                    RentStartTime = new DateTimeOffset(),
                    RentEndTime= new DateTimeOffset()
                }
            }));

            var getHotelCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullHotelReservation(employee.ExternalEmployeeId, trip.ExternalTripId));

            getHotelCall.Returns(Task.FromResult(new DbHotelReservation
            {
                HotelName = "Hotel"
            }));

            var getApartmentCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullApartmentRoomReservation(employee.ExternalEmployeeId, trip.ExternalTripId));

            getApartmentCall.Returns(Task.FromResult((DbRoomReservation)null));

            var info = await _manager.GetFullChecklistInformation(employee.ExternalEmployeeId, trip.ExternalTripId);

            getFlightCall.MustHaveHappenedOnceExactly();

            Assert.True(info.Flight.IsRequired);
            Assert.Equal("new airport address", info.Flight.AirportAddress);
            Assert.Equal("new company", info.Flight.Company);
            Assert.Equal("new flight number", info.Flight.FlightNumber);
            Assert.NotNull(info.Flight.FlightTime);

            Assert.True(info.Flight.IsRequired);
            Assert.Equal("new car address", info.Car.CarAddress);
            Assert.Equal("CAR123", info.Car.CarNumber);
            Assert.NotNull(info.Car.RentStartTime);
            Assert.NotNull(info.Car.RentEndTime);

            Assert.True(info.LivingPlace.IsRequired);
            Assert.True(info.LivingPlace.HotelReservationInfo.Required);
            Assert.False(info.LivingPlace.ApartmentReservationInfo.Required);
            Assert.Equal("Hotel", info.LivingPlace.HotelReservationInfo.HotelName);
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
                RentStartTime = new DateTimeOffset(),
                RentEndTime = new DateTimeOffset()
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
                    RentStartTime = new DateTimeOffset(),
                    RentEndTime = new DateTimeOffset()
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

        [Fact]
        public async Task AddHotelReservationForEmployee_CreatesHotelReservation()
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
                LivingPlace = null
            }));

            var callToCreateHotelReservation = A.CallTo(() => _hotelRepository.CreateEmptyHotelReservation());
            var reservation = await Task.FromResult(new DbHotelReservation());

            var callToCreateLivingPlace = A.CallTo(() => _livingPlaceRepository.CreateHotelLivingPlaceAmenity(reservation.Id));
            callToCreateLivingPlace.Returns(Task.FromResult(new DbLivingPlaceAmenity()
            {
                HotelReservation = new DbHotelReservation()
            }));

            var callToUpdateChecklist =
                A.CallTo(() => _checklistRepository.UpdateChecklist(A<DbEmployeeAmenitiesChecklist>._));

            callToUpdateChecklist.Returns(Task.CompletedTask);

            await _manager.AddHotelReservationForEmployee(employee.ExternalEmployeeId, trip.ExternalTripId);

            getChecklistCall.MustHaveHappenedOnceExactly();
            callToCreateHotelReservation.MustHaveHappenedOnceExactly();
            callToCreateLivingPlace.MustHaveHappenedOnceExactly();
            callToUpdateChecklist.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddHotelReservationForEmployee_ThrowsIfLivingPlaceExists()
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
                LivingPlace = new DbLivingPlaceAmenity()
            }));

            var reservation = new DbHotelReservation
            {
                Id = 1
            };

            var exception = await Assert.ThrowsAsync<ErrorCodeException>(async () =>
            {
                await _manager.AddHotelReservationForEmployee(
                    employee.ExternalEmployeeId,
                    trip.ExternalTripId);
            });

            Assert.Equal(ErrorCodes.ChecklistLivingPlaceAlreadyExists, exception.ErrorCode);

            getChecklistCall.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateHotelReservationForEmployee_UpdatesHotelReservation()
        {
            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId"
            };

            var trip = new DbTrip
            {
                ExternalTripId = "ExternalTripId"
            };

            var getHotelReservationCall = A.CallTo(() =>
                _checklistRepository.GetChecklistFullHotelReservation(employee.ExternalEmployeeId, trip.ExternalTripId));

            getHotelReservationCall.Returns(Task.FromResult(new DbHotelReservation()));

            var hotelReservationInfo = new HotelReservationRequest()
            {
                HotelName = "Hotel",
                DateFrom = new DateTimeOffset(),
                DateTo = new DateTimeOffset()
            };

            var callToUpdateHotelReservation = A.CallTo(() => _hotelRepository.UpdateHotelReservation(A<DbHotelReservation>.That.Matches(c =>
                c.HotelName == hotelReservationInfo.HotelName
                && c.DateFrom != null
                && c.DateTo != null)));

            callToUpdateHotelReservation.Returns(Task.CompletedTask);

            await _manager.UpdateHotelReservationForEmployee(employee.ExternalEmployeeId, trip.ExternalTripId, hotelReservationInfo);

            getHotelReservationCall.MustHaveHappenedOnceExactly();
            callToUpdateHotelReservation.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteHotelReservationForEmployee_DeletesHotelReservation()
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
                LivingPlace = new DbLivingPlaceAmenity()
                {
                    HotelReservation = new DbHotelReservation
                    {
                        Id = 22
                    }
                }
            }));

            var callToUpdateChecklist = A.CallTo(() => _checklistRepository.UpdateChecklist(A<DbEmployeeAmenitiesChecklist>.That.Matches(c =>
                c.LivingPlace == null)));

            callToUpdateChecklist.Returns(Task.CompletedTask);

            var callToDeleteHotelReservation = A.CallTo(() => _hotelRepository.DeleteHotelReservation(A<DbHotelReservation>._));

            callToDeleteHotelReservation.Returns(Task.CompletedTask);

            await _manager.DeleteHotelReservation(employee.ExternalEmployeeId, trip.ExternalTripId);

            getChecklistCall.MustHaveHappenedOnceExactly();
            callToUpdateChecklist.MustHaveHappenedOnceExactly();
            callToDeleteHotelReservation.MustHaveHappenedOnceExactly();
        }
    }
}

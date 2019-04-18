using System;
using BackendApartmentReservation.Database.Entities.Reservations;

namespace BackendApartmentReservation.Tests.Managers
{
    using System.Threading.Tasks;
    using BackendApartmentReservation.Managers;
    using BackendApartmentReservation.Repositories;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities;
    using Database.Entities.Amenities;
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Xunit;

    public class ChecklistManagerTests
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ICarRentRepository _carRentRepository;
        private readonly ILivingPlaceRepository _livingPlaceRepository;

        private readonly IChecklistRepository _checklistRepository;

        private readonly ILogger<ChecklistManager> _logger;

        private readonly ChecklistManager _manager;

        public ChecklistManagerTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _flightRepository = A.Fake<IFlightRepository>();
            _carRentRepository = A.Fake<ICarRentRepository>();
            _livingPlaceRepository = A.Fake<ILivingPlaceRepository>();
            

            _checklistRepository = A.Fake<IChecklistRepository>();

            _logger = new NullLogger<ChecklistManager>();

            _manager = new ChecklistManager(
                _employeeRepository,
                _flightRepository,
                _carRentRepository,
                _livingPlaceRepository,
                _checklistRepository,
                _logger);
        }

        [InlineData(false, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        [Theory]
        public async Task CreateChecklistForEmployee_CreatesProperChecklist(bool flight, bool car, bool livingPlace)
        {
            var employee = new DbEmployee
            {
                Id = 5
            };

            var employeeRepositoryCall = A.CallTo(() => _employeeRepository.GetEmployeeById(employee.Id));

            var checklistRepositoryCall =
                A.CallTo(() => _checklistRepository.AddChecklist(A<DbEmployeeAmenitiesChecklist>._));

            var flightNumber = "FL1234";
            var flightRepositoryCall =
                A.CallTo(() => _flightRepository.CreateFlightAmenityFromFlightNumber(flightNumber));

            var carNumber = "CAR123";
            var carRepositoryCall = A.CallTo(() => _carRentRepository.CreateCarRentAmenityFromCarNumber(carNumber));
            
            //var apartmentRepositoryCall = A.CallTo(() => _livingPlaceRepository.CreateApartmentAmenity("ApartmentString"));
            //var hotelRepositoryCall = A.CallTo(() => _livingPlaceRepository.CreateHotelAmenity("HotelString"));
            var livingPlaceRepositoryCall = A.CallTo(() =>
                _livingPlaceRepository.
                    CreateLivingPlaceAmenity(_livingPlaceRepository.CreateApartmentAmenity("VilniusApartment")
                        .Result, _livingPlaceRepository.CreateHotelAmenity("VilniusHotel").Result)); //TODO: DOESN'T WORK

            if (flight)
            {
                flightRepositoryCall.Returns(new DbFlightAmenity());
            }

            if (car)
            {
                carRepositoryCall.Returns(new DbCarRentAmenity());
            }

            if (livingPlace)
            {
                //apartmentRepositoryCall.Returns(new DbApartmentAmenity());
                //hotelRepositoryCall.Returns(new DbHotelAmenity());
                livingPlaceRepositoryCall.Returns(new DbLivingPlaceAmenity());
            }

            employeeRepositoryCall.Returns(employee);
            checklistRepositoryCall.Returns(Task.CompletedTask);

            // TODO: @tomu figure out logging tests
            //var carLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created car rent amenity for employee {employee.Id}."));

            //var flightLogCall = A.CallTo(
            //    () => _logger.LogInformation($"Created flight amenity for employee {employee.Id}."));

            var checklist = await _manager.CreateChecklistForEmployee(
                employee.Id,
                new FlightReservationInfo
                {
                    Required = flight,
                    FlightNumber = flightNumber
                },
                new CarReservationInfo
                {
                    Required = car,
                    CarNumber = carNumber
                },
                new LivingPlaceReservationInfo
                {
                    Required = livingPlace,
                    ApartmentReservationInfo = new ApartmentReservationInfo { Required = true, ApartmentAddress = "Vilnius" },
                    HotelReservationInfo = new HotelReservationInfo {Required = false, HotelAddress = "Kaunas" }
                });

            employeeRepositoryCall.MustHaveHappenedOnceExactly();
            checklistRepositoryCall.MustHaveHappenedOnceExactly();

            Assert.Equal(checklist.Employee, employee);

            if (car)
            {
                carRepositoryCall.MustHaveHappenedOnceExactly();
                //carLogCall.MustHaveHappened();
            }
            else
            {
                carRepositoryCall.MustNotHaveHappened();
                //carLogCall.MustNotHaveHappened();
            }

            if (flight)
            {
                flightRepositoryCall.MustHaveHappenedOnceExactly();
                //flightLogCall.MustHaveHappened();
            }
            else
            {
                flightRepositoryCall.MustNotHaveHappened();
                //flightLogCall.MustNotHaveHappened();
            }
            if (livingPlace)
            {
                //apartmentRepositoryCall.MustHaveHappened();
                //hotelRepositoryCall.MustHaveHappened();
                livingPlaceRepositoryCall.MustHaveHappened();
            }
            else
            {
                //apartmentRepositoryCall.MustNotHaveHappened();
                //hotelRepositoryCall.MustNotHaveHappened();
                livingPlaceRepositoryCall.MustNotHaveHappened();
            }
        }
    }
}

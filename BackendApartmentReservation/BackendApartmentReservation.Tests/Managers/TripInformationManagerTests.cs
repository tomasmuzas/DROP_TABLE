using BackendApartmentReservation.Confirmations.Interfaces;

namespace BackendApartmentReservation.Tests.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using FakeItEasy;
    using Trips;
    using Trips.Interfaces;
    using Xunit;

    public class TripInformationManagerTests
    {

        [Fact]
        public async Task GetBasicTripInformation_MapsCorrectly()
        {
            var tripId = "tripId";
            var fakeTripRepository = A.Fake<ITripRepository>();
            var fakeConfirmationRepository = A.Fake<IConfirmationRepository>();

            var callToTrip = A.CallTo(() => fakeTripRepository.GetTrip(tripId));

            var trip = new DbTrip
            {
                ExternalTripId = tripId,
                DepartureDate = new DateTimeOffset(new DateTime(2019, 1, 1)),
                ReturnDate = new DateTimeOffset(new DateTime(2019, 1, 1)),
                DestinationOffice = new DbOffice
                {
                    ExternalOfficeId = "ExternalOfficeId",
                    Address = "Address"
                }
            };
            callToTrip.Returns(trip);

            var callToChecklist = A.CallTo(() => fakeTripRepository.GetTripChecklistsWithEmployees(tripId));

            var employee = new DbEmployee
            {
                ExternalEmployeeId = "ExternalEmployeeId",
                Email = "a@a.a",
                FirstName = "a",
                LastName = "b"
            };
            var checklists = new List<DbEmployeeAmenitiesChecklist>
            {
                new DbEmployeeAmenitiesChecklist
                {
                    Car = null,
                    Flight = null,
                    LivingPlace = null,
                    Employee = employee
                }
            };
            callToChecklist.Returns(checklists);

            var callToConfirmations = A.CallTo(() => fakeConfirmationRepository
                .HasAcceptedTripParticipation(employee.ExternalEmployeeId, tripId));

            callToConfirmations.Returns(true);

            var manager = new TripInformationManager(fakeTripRepository, fakeConfirmationRepository);

            var result = await manager.GetBasicTripInformation(tripId);

            callToTrip.MustHaveHappenedOnceExactly();
            callToChecklist.MustHaveHappenedOnceExactly();
            callToConfirmations.MustHaveHappenedOnceExactly();

            Assert.NotNull(result);
            Assert.Equal(tripId, result.TripId);
            Assert.Equal(trip.DepartureDate, result.StartTime);
            Assert.Equal(trip.ReturnDate, result.EndTime);
            Assert.NotEmpty(result.ChecklistInfos);
            var checklistInfo = result.ChecklistInfos[0];
            Assert.Equal(checklistInfo.Employee.Id, employee.ExternalEmployeeId);
            Assert.Equal(checklistInfo.Employee.FirstName, employee.FirstName);
            Assert.Equal(checklistInfo.Employee.LastName, employee.LastName);
            Assert.Equal(checklistInfo.Employee.Email, employee.Email);
            Assert.True(checklistInfo.HasAcceptedTripConfirmation);
            Assert.False(checklistInfo.IsApartmentRequired);
            Assert.False(checklistInfo.IsFlightRequired);
            Assert.False(checklistInfo.IsCarRentRequired);

        }
    }
}

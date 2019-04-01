using System;
using System.Threading.Tasks;

using BackendApartmentReservation.Managers;
using BackendApartmentReservation.Repositories;

using FakeItEasy;

using Xunit;

namespace BackendApartmentReservation.Tests
{
    public class EmployeeManagerTests
    {
        [Fact]
        public async Task CreateUser_SavesAndReturnsCorrectId()
        {
            var fakeEmployeeRepository = A.Fake<IEmployeeRepository>();

            var name = "Name";
            var surname = "Surname";
            var email = "Email";
            var office = "Office";

            var manager = new EmployeeManager(fakeEmployeeRepository);

            var id = await manager.CreateEmployee(name, surname, email, office);

            A.CallTo(() => fakeEmployeeRepository.CreateEmployee(name, surname, email, office))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeEmployeeRepository.SaveChanges())
                .MustHaveHappenedOnceExactly();

            Assert.NotEmpty(id);
        }
    }
}

namespace BackendApartmentReservation.Tests.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Managers;
    using BackendApartmentReservation.Repositories;
    using Database.Entities;
    using FakeItEasy;
    using Xunit;

    public class EmployeeManagerTests
    {
        [Fact]
        public async Task CreateUser_SavesAndReturnsCorrectId()
        {
            var fakeEmployeeRepository = A.Fake<IEmployeeRepository>();

            var name = "Name";
            var surname = "Surname";
            var email = "Email";
            var office = new DbOffice();

            var manager = new EmployeeManager(fakeEmployeeRepository);

            var dbEmployee = new DbEmployee();
            dbEmployee.FirstName = name;
            dbEmployee.LastName = surname;
            dbEmployee.Email = email;
            dbEmployee.Office = office;

            var id = await manager.CreateEmployee(dbEmployee);

            A.CallTo(() => fakeEmployeeRepository.CreateEmployee(dbEmployee))
                .MustHaveHappenedOnceExactly();

            Assert.NotEmpty(id);
        }

        [Fact]
        public async Task GetAllEmployees_MapsListsCorrectly()
        {
            var fakeEmployeeRepository = A.Fake<IEmployeeRepository>();
            var fakeEmployeeList = new List<DbEmployee>
            {
                new DbEmployee
                {
                    FirstName = "Name1",
                    LastName = "Surname1",
                    Email = "Email1",
                    ExternalEmployeeId = "id1"
                },
                new DbEmployee
                {
                    FirstName = "Name2",
                    LastName = "Surname2",
                    Email = "Email2",
                    ExternalEmployeeId = "id2"
                }
            };

            A.CallTo(() => fakeEmployeeRepository.GetAllEmployees())
                .Returns(fakeEmployeeList);

            var manager = new EmployeeManager(fakeEmployeeRepository);

            var responseEmployees = await manager.GetAllEmployees();

            A.CallTo(() => fakeEmployeeRepository.GetAllEmployees())
                .MustHaveHappenedOnceExactly();

            foreach (var employee in fakeEmployeeList)
            {
                responseEmployees.Single(e =>
                    e.Id == employee.ExternalEmployeeId
                    && e.FirstName == employee.FirstName
                    && e.LastName == employee.LastName
                    && e.Email == employee.Email);
            }
        }
    }
}

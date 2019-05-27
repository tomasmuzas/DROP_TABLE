

namespace BackendApartmentReservation.Tests.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database.Entities;
    using FakeItEasy;
    using Offices;
    using Offices.Interfaces;
    using Xunit;

    public class OfficeManagerTests
    {
        [Fact]
        public async Task GetAllOffices_MapsListsCorrectly()
        {
            var fakeOfficeRepository = A.Fake<IOfficeRepository>();
            var fakeOfficeList = new List<DbOffice>
            {
                new DbOffice
                {
                    Address = "Address 1",
                    ExternalOfficeId = "Id1",
                    Id = 1
                },
                new DbOffice
                {
                    Address = "Address 2",
                    ExternalOfficeId = "Id2",
                    Id = 2
                }
            };

            A.CallTo(() => fakeOfficeRepository.GetAllOffices())
                .Returns(fakeOfficeList);

            var manager = new OfficeManager(fakeOfficeRepository);

            var responseEmployees = await manager.GetAllOffices();

            A.CallTo(() => fakeOfficeRepository.GetAllOffices())
                .MustHaveHappenedOnceExactly();

            foreach (var office in fakeOfficeList)
            {
                responseEmployees.Single(o =>
                    o.Id == office.ExternalOfficeId
                    && o.Address == office.Address);
            }
        }
    }
}

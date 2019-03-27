using System;
using BackendApartmentReservation.Database;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Tests
{
    public class TestBase
    {
        public DatabaseContext GetNewDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DatabaseContext(options);
        }
    }
}

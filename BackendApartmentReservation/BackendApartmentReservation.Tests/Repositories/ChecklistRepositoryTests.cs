namespace BackendApartmentReservation.Tests.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using BackendApartmentReservation.Repositories.Checklist;
    using Database.Entities;
    using Xunit;

    public class ChecklistRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task AddChecklist_Success()
        {
            using (var dbContext = GetNewDatabaseContext())
            {
                var repository = new ChecklistRepository(dbContext);
                var checklist = new DbEmployeeAmenitiesChecklist();

                await repository.AddChecklist(checklist);

                var checklistEntry = dbContext.Checklists.Single();

                Assert.NotEqual(0, checklistEntry.Id);
            }
        }
    }
}

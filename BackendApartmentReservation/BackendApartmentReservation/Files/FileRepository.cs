namespace BackendApartmentReservation.Employees
{
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Interfaces;

    public class FileRepository : IFileRepository
    {
        private readonly DatabaseContext _context;

        public FileRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DbFile> GetFileById(int fileID)
        {
            return await _context.Files.FindAsync(fileID);
        }

        public async Task CreateFile(DbFile dbFile)
        {             
            await _context.Files.AddAsync(dbFile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFile(DbFile dbFile)
        {
            _context.Files.Update(dbFile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFile(DbFile dbFile)
        {
            _context.Files.Remove(dbFile);
            await _context.SaveChangesAsync();
        }
    }
}
namespace BackendApartmentReservation.Employees
{
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class FileRepository : IFileRepository
    {
        private readonly DatabaseContext _context;

        public FileRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DbFile> GetFileById(string fileID)
        {
            return await _context.Files.FirstOrDefaultAsync(f => f.ExternalFileId == fileID);
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
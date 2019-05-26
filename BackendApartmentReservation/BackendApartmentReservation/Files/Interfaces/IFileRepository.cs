namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IFileRepository
    {
        Task<DbFile> GetFileById(string fileID);

        Task<DbFile> CreateFile(DbFile dbFile);

        Task UpdateFile(DbFile dbFile);

        Task DeleteFile(DbFile dbFile);
    }
}
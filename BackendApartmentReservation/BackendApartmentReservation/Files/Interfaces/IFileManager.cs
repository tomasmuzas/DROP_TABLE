namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IFileManager
    {
        Task<string> UploadFile(DbFile dbFile);

        Task<DbFile> GetFileById(string fileID);
    }
}
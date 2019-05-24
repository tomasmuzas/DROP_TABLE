namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Threading.Tasks;
    using Database.Entities;
    using Microsoft.AspNetCore.Http;

    public interface IFileManager
    {
        Task<string> UploadFile(IFormFile formFile);

        Task<DbFile> GetFileById(string fileID);
    }
}
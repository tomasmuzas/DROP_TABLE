namespace BackendApartmentReservation.Employees
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Database.Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Http;

    public class FileManager : IFileManager
    {
        private readonly IFileRepository _fileRepository;

        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<DbFile> UploadFile(IFormFile formFile)
        {
            var fileId = Guid.NewGuid().ToString();
            var file = new DbFile();
            file.ExternalFileId = fileId;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                file.File = stream.ToArray();
            }

            return await _fileRepository.CreateFile(file);
        }

        public async Task<DbFile> GetFileById(string fileID)
        {
            return await _fileRepository.GetFileById(fileID);
        }

        public async Task DeleteFile(DbFile file)
        {
            await _fileRepository.DeleteFile(file);
        }
    }
}
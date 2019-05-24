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

        public async Task<string> UploadFile(IFormFile formFile)
        {
            var fileId = Guid.NewGuid().ToString();
            var file = new DbFile();
            file.ExternalFileId = fileId;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                file.File = stream.ToArray();
            }

            await _fileRepository.CreateFile(file);
            return fileId;
        }

        public async Task<DbFile> GetFileById(string fileID)
        {
            return await _fileRepository.GetFileById(fileID);
        }
    }
}
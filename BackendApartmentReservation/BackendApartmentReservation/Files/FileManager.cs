namespace BackendApartmentReservation.Employees
{
    using System;
    using System.Threading.Tasks;
    using Database.Entities;
    using Interfaces;

    public class FileManager : IFileManager
    {
        private readonly IFileRepository _fileRepository;

        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string> UploadFile(DbFile dbFile)
        {
            var fileId = Guid.NewGuid().ToString();
            dbFile.ExternalFileId = fileId;

            await _fileRepository.CreateFile(dbFile);
            return fileId;
        }

        public async Task<DbFile> GetFileById(string fileID)
        {
            return await _fileRepository.GetFileById(fileID);
        }
    }
}
using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Employees
{
    using System.Threading.Tasks;
    using Database.Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using Microsoft.AspNetCore.Authorization;
    
    [ApiController]
    public class FilesController : AuthorizedController
    {
        private readonly IFileManager _fileManager;


        public FilesController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost]
        [OrganizerOnly]
        [Route("files")]
        public async Task<IActionResult> Create(IFormFile file)
        {
            long size = file.Length;
            var filePath = Path.GetTempFileName();

            var newFile = new DbFile();

            if (file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    newFile.File = stream.ToArray();
                    await _fileManager.UploadFile(newFile);
                }
            }

            return Ok(new { size, filePath });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("files/{fileId}")]
        public async Task<IActionResult> GetFileById(string fileID)
        {
            var file = await _fileManager.GetFileById(fileID);
            
            return File(file.File, "application/pdf", "newFile.pdf");
        }

    }
}
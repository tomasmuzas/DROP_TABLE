using BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly;
using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.IO;
    using Microsoft.AspNetCore.Authorization;

    [Route("api")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;


        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        //[OrganizerOnly] // Comment out when working locally
        [Route("files")]
        public async Task<IActionResult> Create(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                var file = new DbFile();

                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        file.File = stream.ToArray();
                        await _fileRepository.CreateFile(file);
                    }
                }
            }

            return Ok(new { count = files.Count, size, filePath });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("files/{fileId}")]
        public async Task<IActionResult> GetFileById(int fileID)
        {
            var file = await _fileRepository.GetFileById(fileID);

            return Ok(File(file.File, "application/pdf"));
        }

    }
}
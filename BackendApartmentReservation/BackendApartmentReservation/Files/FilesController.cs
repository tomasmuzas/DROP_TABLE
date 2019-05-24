using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Employees
{
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    public class FilesController : AuthorizedController
    {
        private readonly IFileManager _fileManager;


        public FilesController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        [Route("files/{fileId}")]
        public async Task<IActionResult> GetFileById(string fileID)
        {
            var file = await _fileManager.GetFileById(fileID);
            
            return File(file.File, "application/pdf", "document.pdf");
        }

    }
}
using BackendApartmentReservation.Infrastructure.Authorization;

namespace BackendApartmentReservation.Employees
{
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("files")]
    public class FilesController : AuthorizedController
    {
        private readonly IFileManager _fileManager;


        public FilesController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{fileId}")]
        public async Task<IActionResult> GetFileById(string fileId)
        {
            var file = await _fileManager.GetFileById(fileId);

            return File(file.File, "application/pdf", "document.pdf");
        }

    }
}
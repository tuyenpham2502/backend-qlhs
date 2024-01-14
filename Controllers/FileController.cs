

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Filters;
using QlhsServer.Helpers;
using QlhsServer.Models;
using QlhsServer.Models.Response;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileRepository fileRepo;

        public FileStorageController(IFileRepository repo)
        {
            fileRepo = repo;
        }

        [HttpPost("UploadFile")]
        [Authorize]
        public async Task<IActionResult> FileStorage([FromForm] FileModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var result = await fileRepo.UploadIFileAsync(model, userId);
            return AppResult.ActionResult(result);

        }

        [HttpGet("GetFile/{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {

            var result = await fileRepo.GetFileAsync(fileName);

            var contentType = "application/octet-stream";

            if (result != null)
            {
                return File(result, contentType);
            }

            return NotFound();

        }

    }
}
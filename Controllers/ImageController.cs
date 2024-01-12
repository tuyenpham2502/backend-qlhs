

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Filters;
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


            var result = await fileRepo.UploadFileAsync(model, userId);
            if (result is SuccessModel)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("GetFile/{imageName}")]
        public async Task<IActionResult> GetFile(string imageName)
        {
            try
            {
                var result = await fileRepo.GetFileAsync(imageName);

                return File(result, "image/jpeg");
            }
            catch (FileNotFoundException)
            {
                return NotFound("File not found");
            }


        }
    }
}
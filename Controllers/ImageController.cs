

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IFileRepository fileRepo;

        public UploadFileController(IFileRepository repo)
        {
            fileRepo = repo;
        }

        [HttpPost("UploadFile")]
        public async Task<object> UploadFile([FromForm] FileRequestModel model)
        {
            var result = await fileRepo.UploadFileAsync(model);
            return Ok(result);
        }

        [HttpGet("GetFile/{imageName}")]
        public async Task<FileContentResult> GetFile(string imageName)
        {
            var result = await fileRepo.GetFileAsync(imageName);
            return File(result, "image/jpeg");
        }
    }
}
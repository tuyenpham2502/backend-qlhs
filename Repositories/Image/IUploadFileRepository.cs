using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;
using QlhsServer.Models.Response;

namespace QlhsServer.Repositories
{
    public interface IFileRepository
    {
        public Task<RequestResponse> UploadFileAsync([FromForm] FileModel model, string userId);

        public Task<byte []> GetFileAsync(string imageName);
    }
}
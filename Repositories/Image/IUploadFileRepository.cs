using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;

namespace QlhsServer.Repositories
{
    public interface IFileRepository
    {
        public Task<object> UploadFileAsync([FromForm] FileRequestModel model);

        public Task<byte []> GetFileAsync(string imageName);
    }
}
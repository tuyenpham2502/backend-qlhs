using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;

namespace QlhsServer.Repositories
{
    public interface IFileRepository
    {
        public Task<object> FileStorageAsync([FromForm] FileModel model, string userId);

        public Task<byte []> GetFileAsync(string imageName);
    }
}
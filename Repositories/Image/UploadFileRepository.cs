
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;

namespace QlhsServer.Repositories
{
    public class FileStorageRepository : IFileRepository
    {

        public Task<object> FileStorageAsync([FromForm] FileModel model, string userId)
        {
                Random random = new Random();

                int randomNumber = random.Next(100000, 999999);
                string fileExtension = Path.GetExtension(model.File.FileName);
                string fileName = userId + "_" + randomNumber + fileExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                return Task.FromResult<object>(new { originName = model.File.FileName, fileName });
            }
            

        public Task<byte[]> GetFileAsync(string imageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "images", imageName);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }

            byte[] b = File.ReadAllBytes(path);

            return Task.FromResult<byte[]>(b);

        }

    }

}
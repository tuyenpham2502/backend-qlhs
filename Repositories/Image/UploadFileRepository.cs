
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;

namespace QlhsServer.Repositories
{
    public class UploadFileRepository : IFileRepository
    {

        public Task<object> UploadFileAsync([FromForm] FileRequestModel model)
        {

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "images", model.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                return Task.FromResult<object>(new { path = path });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public Task<byte []> GetFileAsync(string imageName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "images", imageName);

               if(!File.Exists(path))
                {
                    return null;
                }

                byte [] b = File.ReadAllBytes(path);

                return Task.FromResult<byte []>(b);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




    }
}
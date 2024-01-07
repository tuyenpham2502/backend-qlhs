
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;
using QlhsServer.Models.Response;

namespace QlhsServer.Repositories
{
    public class FileStorageRepository : IFileRepository
    {

        public Task<RequestResponse> UploadFileAsync([FromForm] FileModel model, string userId)
        {
            try
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
                return Task.FromResult<RequestResponse>(new SuccessModel
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Upload file successfully",
                    Data = new { fileName, originalName = model.File.FileName }
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<RequestResponse>(new ErrorModel
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = ex.Source,
                            Message = ex.Message
                        }
                    }
                });
            }
        }


        public Task<byte[]> GetFileAsync(string imageName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "images", imageName);

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("File not found");
                }

                byte[] b = File.ReadAllBytes(path);

                return Task.FromResult<byte[]>(b);
            }
            catch (Exception ex)
            {
                return Task.FromResult<byte[]>(null);
            }
        }

    }

}
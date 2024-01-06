namespace QlhsServer.Models
{
    public class FileRequestModel
    {
        public string FileName { get; set; } = null!;

        public IFormFile File { get; set; } = null!;
    }
}
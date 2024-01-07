using System.ComponentModel.DataAnnotations;

namespace QlhsServer.Models
{
    public class FileModel
    {
        [Required]
        public IFormFile File { get; set; } = null!;
        

        
    }
}
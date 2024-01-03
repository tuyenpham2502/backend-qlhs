using System.ComponentModel.DataAnnotations;

namespace QlhsServer.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }

    }
}
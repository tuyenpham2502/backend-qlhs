using System.ComponentModel.DataAnnotations;

namespace QlhsServer.Models
{
    public class UserModel
    {
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Avatar { get; set; }

        [Phone]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "The phone number must be between 9 and 15 characters.")]
        public string PhoneNumber { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

    }
}


namespace QlhsServer.Models
{
    public class UserResponseModel
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IList<string> Roles { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}


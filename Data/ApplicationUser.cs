using Microsoft.AspNetCore.Identity;

namespace QlhsServer.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Avatar { get; set; } = null!;
        public string? Address { get; set; } = null!;
    }
}

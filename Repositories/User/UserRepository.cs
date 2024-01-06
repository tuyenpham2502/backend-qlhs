using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QlhsServer.Data;
using QlhsServer.Models;
    using System.Security.Claims;
namespace QlhsServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;


        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<object> GetUsersAsync(HttpContext ctx)
        {
            try {
                
                string token = ctx.Request.Headers["Authorization"];

                if (token != null && token.StartsWith("Bearer "))
                {
                    token = token.Substring("Bearer ".Length).Trim();
                }

                var handler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwt = handler.ReadJwtToken(token) as JwtSecurityToken;

                var claims = new Dictionary<string, string>();

                foreach (var claim in jwt.Claims)
                {
                    claims.Add(claim.Type, claim.Value);
                }

                var user = await userManager.FindByIdAsync(claims[ClaimTypes.NameIdentifier]);
                var userRoles = await userManager.GetRolesAsync(user);  
                var users = await userManager.Users.ToListAsync();
                System.Console.WriteLine(users);
                return new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = userRoles,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

            }
            catch(Exception ex)
            {
                return new {
                    error = ex.Message,
                };
            }


        }
    }
}
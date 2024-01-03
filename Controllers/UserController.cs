using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository repo)
        {
            userRepo = repo;
        }

        [HttpGet("UserProfiles")]
        [Authorize]
        public async Task<object> GetUsers()
        {
            var result = await userRepo.GetUsersAsync(HttpContext);

            return Ok(result);
        }
    }
}
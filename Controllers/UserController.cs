using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> GetUsers()
        {
            var result = await userRepo.GetUsersAsync(HttpContext);

            return Ok(result);
        }
    }
}
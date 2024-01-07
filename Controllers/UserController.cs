using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;
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

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await userRepo.GetUserAsync(userId);

            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserModel model)
        {


            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            {
                var result = await userRepo.UpdateUserAsync(model, userId);

                return Ok(result);
            }
        }
    }

}

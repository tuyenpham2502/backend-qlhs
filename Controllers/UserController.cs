using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Filters;
using QlhsServer.Models;
using QlhsServer.Models.Response;
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

            if (result is SuccessModel)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            System.Console.WriteLine(userId);

            var result = await userRepo.UpdateUserAsync(model, userId);
            
            if (result is SuccessModel)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }

}
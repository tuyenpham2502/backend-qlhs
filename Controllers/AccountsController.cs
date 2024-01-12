using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using QlhsServer.Filters;
using QlhsServer.Helpers;
using QlhsServer.Models;
using QlhsServer.Models.Response;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;

        public AccountsController(IAccountRepository repo)
        {
            accountRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);

            return AppResult.ActionResult(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            RequestResponse result = await accountRepo.SignInAsync(signInModel);

            return AppResult.ActionResult(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await accountRepo.LogoutAsync();

            if (result is SuccessModel)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

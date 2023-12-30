using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QlhsServer.Models;
using QlhsServer.Repositories;

namespace QlhsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;

        public AccountsController(IAccountRepository repo)
        {
            accountRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<object> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);
            
            return Ok(result);
        }

        [HttpPost("SignIn")]
        public async Task<object> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using QlhsServer.Models;

namespace QlhsServer.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}

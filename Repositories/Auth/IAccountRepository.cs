using QlhsServer.Models;

namespace QlhsServer.Repositories
{
    public interface IAccountRepository
    {
        public Task<object> SignUpAsync(SignUpModel model);
        public Task<object> SignInAsync(SignInModel model);
    }
}

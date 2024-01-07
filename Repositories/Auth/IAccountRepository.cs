using QlhsServer.Models;
using QlhsServer.Models.Response;

namespace QlhsServer.Repositories
{
    public interface IAccountRepository
    {
        public Task<RequestResponse> SignUpAsync(SignUpModel model);
        public Task<RequestResponse> SignInAsync(SignInModel model);
    }
}

using QlhsServer.Models;
using QlhsServer.Models.Response;

namespace QlhsServer.Repositories
{
    public interface IUserRepository
    {
        public Task<RequestResponse> GetUserAsync( string userId);

        public Task<RequestResponse> UpdateUserAsync(UserModel model, string userId);
    }
}
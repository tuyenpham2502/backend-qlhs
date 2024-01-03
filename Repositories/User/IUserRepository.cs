namespace QlhsServer.Repositories
{
    public interface IUserRepository
    {
        public Task<object> GetUsersAsync( HttpContext ctx);
    }
}
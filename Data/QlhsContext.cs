using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QlhsServer.Data
{
    public class QlhsContext : IdentityDbContext<ApplicationUser>
    {
        public QlhsContext(DbContextOptions<QlhsContext> opt) : base(opt)
        {

        }

        #region DbSet
        #endregion
    }
}

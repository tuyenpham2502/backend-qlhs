using Microsoft.AspNetCore.Identity;
using QlhsServer.Data;

namespace QlhsServer.DependencyInjection
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<QlhsContext>().AddDefaultTokenProviders();
        }
    }
}
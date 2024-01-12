using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QlhsServer.Data;

namespace QlhsServer.DependencyInjection {
    public static class EntityFrameworkConfiguration
    {
        public static void AddEFconfiguration(this IServiceCollection services, IConfiguration configuration) {
            
            services.AddDbContext<QlhsContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("QlhsStore"));
            });
        }
    }
}
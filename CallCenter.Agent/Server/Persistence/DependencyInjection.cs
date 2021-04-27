using CallCenter.Agent.Server.Application.Interfaces;
using CallCenter.Agent.Server.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CallCenter.Agent.Server.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CallCenterDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CallCenterDbConnection")));
            services.AddScoped<ICallCenterDbContext>(provider => provider.GetService<CallCenterDbContext>());

            return services;
        }
    }
}

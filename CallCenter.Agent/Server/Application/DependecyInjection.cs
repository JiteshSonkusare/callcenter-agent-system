using System.Reflection;
using CallCenter.Agent.Server.Application.Agent.Queries;
using CallCenter.Agent.Server.Common.Behaviours;
using CallCenter.Agent.Server.Common.Interfaces;
using CallCenter.Agent.Server.Common.Mappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CallCenter.Agent.Server.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped<IMapper<Shared.Models.Agent, AgentDto>, AgentMapper>();

            return services;
        }
    }
}

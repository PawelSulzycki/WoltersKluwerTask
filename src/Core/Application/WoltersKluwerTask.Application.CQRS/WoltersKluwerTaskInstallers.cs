using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WoltersKluwerTask.Application.CQRS
{
    public static class WoltersKluwerTaskInstallers
    {
        public static IServiceCollection AddWoltersKluwerTaskCQRS(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

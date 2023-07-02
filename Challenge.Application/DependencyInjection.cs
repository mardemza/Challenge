using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Challenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDiApplication(this IServiceCollection services)
        {
            // -- Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // -- Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

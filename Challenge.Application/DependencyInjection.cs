using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Challenge.Application.Permissions.Services;
using Challenge.EntityFramework.Persistence;
using Nest;
using AutoMapper;
using Challenge.Application.Permissions.Services.Dto;

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

        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            // -- Get configs
            var url = configuration["Elasticsearch:Url"];
            var indexName = configuration["Elasticsearch:IndexName"];

            // -- Generate connections
            var settings = new ConnectionSettings(new Uri(url))
                                .DefaultIndex(indexName);

            // -- Create client connection
            var client = new ElasticClient(settings);

            // -- Config singleton and scoped
            services.AddSingleton<IElasticClient>(client);
            services.AddSingleton<IEslasticsearchService, EslasticsearchService>();
        }

        public static IServiceProvider RunRefreshOnElasticsearch(this IServiceProvider services)
        {
            // -- Get Scope and Context
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChallengeDbContext>();
            var elasticSearchService = scope.ServiceProvider.GetRequiredService<IEslasticsearchService>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            elasticSearchService.Reset().Wait();

            var employees = context.Employees.ToList();
            foreach (var item in employees)
            {
                var obj = mapper.Map<EmployeeDto>(item);
                elasticSearchService.Add(obj).Wait();
            }

            var permissions = context.Permissions.ToList();
            foreach (var item in permissions)
            {
                var obj = mapper.Map<PermissionDto>(item);
                elasticSearchService.Add(obj).Wait();
            }

            var permissionTypes = context.PermissionTypes.ToList();
            foreach (var item in permissionTypes)
            {
                var obj = mapper.Map<PermissionTypeDto>(item);
                elasticSearchService.Add(obj).Wait();
            }

            return services;
        }
    }
}

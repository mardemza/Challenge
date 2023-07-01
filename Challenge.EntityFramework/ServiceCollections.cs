using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.EntityFramework
{
    public static class ServiceCollections
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            // -- Add DbContext
            services.AddDbContext<ChallengeDbContext>();

            return services;
        }

        public static IServiceProvider RunMigrations(this IServiceProvider services)
        {
            
            using (var Scope = services.CreateScope())
            {
                var context = Scope.ServiceProvider.GetRequiredService<ChallengeDbContext>();
                context.Database.Migrate();
            }
            return services;
        }
    }
}

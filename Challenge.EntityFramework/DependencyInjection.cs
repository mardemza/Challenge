using Challenge.EntityFramework.Interfaces;
using Challenge.EntityFramework.Persistence;
using Challenge.EntityFramework.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.EntityFramework
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChallengeDbContext>(options =>
                    options.UseSqlite(
                        configuration.GetConnectionString("Default"),
                        b => b.MigrationsAssembly(typeof(ChallengeDbContext).Assembly.FullName)));
            
            services.AddScoped<IChallengeDbContext>(provider => provider.GetRequiredService<ChallengeDbContext>());

            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IReadRepository<>), typeof(BaseRepository<>));            

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

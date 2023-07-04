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
        public static IServiceCollection AddDiEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChallengeDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("Default"),
                        b => b.MigrationsAssembly(typeof(ChallengeDbContext).Assembly.FullName)));

            services.AddScoped<IChallengeDbContext>(provider => provider.GetRequiredService<ChallengeDbContext>());

            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IReadRepository<>), typeof(BaseRepository<>));

            return services;
        }

        public static IServiceProvider RunMigrations(this IServiceProvider services)
        {
            // -- Get Scope and Context
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChallengeDbContext>();

            // -- Apply Migrations
            context.Database.Migrate();

            // -- Apply Seed
            context.Seed();

            return services;
        }
    }
}

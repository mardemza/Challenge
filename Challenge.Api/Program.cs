
using Serilog;
using Challenge.EntityFramework;
using Challenge.Application;

namespace Challenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // -- Init WebApplication
            var builder = WebApplication.CreateBuilder(args);

            // -- Use Serilog
            builder.Host.UseSerilog((hostContext, services, configuration) => {
                configuration.WriteTo.Console();
                configuration.WriteTo.File("App_Data/Logs.txt");
            });

            // -- Add DbContext, Pattern Repository and Unit Of Work
            builder.Services.AddDiEntityFramework(builder.Configuration);

            // -- Add MediatR, AutoMapper and CQRS
            builder.Services.AddDiApplication();

            // -- Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // -- Run Aplication
            var app = builder.Build();

            // -- Run Migrations
            app.Services.RunMigrations();

            // -- Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
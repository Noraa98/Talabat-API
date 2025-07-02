using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        // Entry point for the application.
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers(); // Register Required Services by AspNet Core
                                                             // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // webApplicationBuilder.Services.AddOpenApi(); 
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

            #endregion

            #region Update Database and Data Seeding

            var app = webApplicationBuilder.Build();

            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<StoreContext>();
            // Ask The Runtime env for an object from "StoreContext" service explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync();// Apply Migrations if any [UPdate Database Schema]
           
                 await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during apply yhe migration.");
            }

            #endregion

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}

using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.MiddleWares;
using LinkDev.Talabat.Application;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
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

            webApplicationBuilder.Services
                .AddControllers()
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly)
                .ConfigureApiBehaviorOptions(options=>
                {
                    options.SuppressModelStateInvalidFilter = false; // the default behavior
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .Select(p => new ApiValidationErrorResponse.ValidationError()
                        {
                            Field = p.Key,
                            Errors = p.Value!.Errors.Select(e => e.ErrorMessage)
                        });
                        var errorResponse = new ApiValidationErrorResponse
                        {
                            Errors = errors
                        };
                        return new BadRequestObjectResult(errorResponse);
                    };
                }
                ); // Register Required Services by AspNet Core
                                                             // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // webApplicationBuilder.Services.AddOpenApi(); 
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddApplicationServices();

           
            

            #endregion

            var app = webApplicationBuilder.Build();

            #region  Databases Initalization

            await app.InitializeStoreContextAsync();

            #endregion

            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}

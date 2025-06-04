
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        // Entry point for the application.
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers(); // Register Required Services by AspNet Core
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            webApplicationBuilder.Services.AddOpenApi(); 
            #endregion

            var app = webApplicationBuilder.Build();

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}

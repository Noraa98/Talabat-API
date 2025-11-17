using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.MiddleWares
{
    // convension based
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                //if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                //{
                //    var response = new ApiResponse((int)HttpStatusCode.NotFound, "The Requested EndPoint: not found");
                //    await context.Response.WriteAsync(response.ToString()!);
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                ApiResponse response;
                switch(ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        response = new ApiResponse((int)HttpStatusCode.NotFound, ex.Message);
                        break;
                    case BadRequestException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response = new ApiResponse((int)HttpStatusCode.BadRequest, ex.Message);
                        break;
                    //case Application.Exceptions.ValidationException validationException:
                    //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //    break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response = _env.IsDevelopment()
                    ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                        break;
                }


                context.Response.ContentType = "application/json";

               
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}

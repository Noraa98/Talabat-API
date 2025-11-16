using System.Text.Json;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Unauthorized",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => null
            };
        }


        // override ToString for json output
        public override string? ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }

}

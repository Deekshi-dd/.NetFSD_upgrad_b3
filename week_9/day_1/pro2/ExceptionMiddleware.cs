using System.Net;
using System.Text.Json;
using WebApplication17.Exceptions;
using WebApplication17.Models;

namespace WebApplication17.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Something went wrong";

            if (ex is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = ex.Message;
            }
            else if (ex is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
            }

            var response = new ErrorResponse
            {
                Message = message,
                StatusCode = (int)statusCode,
                Timestamp = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

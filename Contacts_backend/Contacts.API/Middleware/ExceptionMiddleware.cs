using Contacts.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Contacts.API.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object response;
            HttpStatusCode statusCode;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case DuplicateContactException:
                    statusCode = HttpStatusCode.Conflict;
                    break;

                case DomainValidationException validationException:

                    statusCode = HttpStatusCode.BadRequest;

                    response = new
                    {
                        type = $"https://httpstatuses.com/{(int)statusCode}",
                        title = "Validation failed",
                        status = (int)statusCode,
                        errors = validationException.Errors,
                        traceId = context.TraceIdentifier
                    };

                    context.Response.StatusCode = (int)statusCode;
                    context.Response.ContentType = "application/json";

                    return context.Response.WriteAsync(
                        JsonSerializer.Serialize(response));

                case InfrastructureOperationException:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;

                case DbUpdateConcurrencyException:
                    statusCode = HttpStatusCode.Conflict;
                    break;

                case DbUpdateException:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            response = new
            {
                type = $"https://httpstatuses.com/{(int)statusCode}",
                title = ex.GetType().Name,
                status = (int)statusCode,
                detail = _env.IsDevelopment()
                    ? ex.Message
                    : "An unexpected error occurred",
                traceId = context.TraceIdentifier
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}

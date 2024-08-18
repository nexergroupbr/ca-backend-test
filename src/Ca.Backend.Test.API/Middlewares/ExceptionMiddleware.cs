using System.Net.Mime;
using System.Text.Json;
using Ca.Backend.Test.API.Models.Response.Api;
using FluentValidation;

namespace Ca.Backend.Test.API.Middlewares;
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
        catch (ValidationException e)
        {
            var response = new GenericHttpResponse<object>
            {
                Errors = e.Errors.Select(error => error.ErrorMessage),
                StatusCode = StatusCodes.Status400BadRequest,
                Data = null
            };

            _logger.LogWarning("Validation exception occurred: {Errors}", response.Errors);
            await BuildResponseAsync(context, response.StatusCode, JsonSerializer.Serialize(response), MediaTypeNames.Application.Json);
        }
        catch (HttpRequestException ex)
        {
            var response = new GenericHttpResponse<object>
            {
                Errors = new[] { "The service is temporarily unavailable. Please try again later." },
                StatusCode = StatusCodes.Status400BadRequest,
                Data = null
            };

            _logger.LogError(ex, "HTTP request exception occurred: The service is temporarily unavailable. Please try again later.");
            await BuildResponseAsync(context, response.StatusCode, JsonSerializer.Serialize(response), MediaTypeNames.Application.Json);
        }
        catch (ApplicationException ex)
        {
            var response = new GenericHttpResponse<object>
            {
                Errors = new[] { ex.Message },
                StatusCode = StatusCodes.Status400BadRequest,
                Data = null
            };

            _logger.LogError(ex, "Application exception occurred: {Message}", ex.Message);
            await BuildResponseAsync(context, response.StatusCode, JsonSerializer.Serialize(response), MediaTypeNames.Application.Json);
        }
        catch (Exception ex)
        {
            var response = new GenericHttpResponse<object>
            {
                Errors = new[] { "An unexpected error occurred." },
                StatusCode = StatusCodes.Status400BadRequest,
                Data = null
            };

            _logger.LogError(ex, "An unexpected exception occurred: {Message}", ex.Message);
            await BuildResponseAsync(context, response.StatusCode, JsonSerializer.Serialize(response), MediaTypeNames.Application.Json);
        }
    }

    private async Task BuildResponseAsync(HttpContext context, int statusCode, string body, string contentType)
    {
        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = contentType;
        await context.Response.WriteAsync(body);
    }
}


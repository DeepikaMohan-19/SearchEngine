using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation("Handling request: {method} {url}", httpContext.Request.Method, httpContext.Request.Path);

        var request = httpContext.Request;
        var body = string.Empty;
        if (request.Body.CanSeek)
        {
            request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(request.Body))
            {
                body = await reader.ReadToEndAsync();
            }
            request.Body.Seek(0, SeekOrigin.Begin);
        }

        _logger.LogInformation("Request Body: {body}", body);

        await _next(httpContext);
    }
}

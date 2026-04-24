using System.Diagnostics;

namespace br.com.fiap.cloudgames.WebAPI.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private const String CorrelationHeaderName = "X-Correlation-ID";

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = GetOrCreateCorrelationId(context);
        context.Response.Headers[CorrelationHeaderName] = requestId;
        
        var method = context.Request.Method;
        var path = context.Request.Path;
        String? userId = context.User?.Identity?.Name;
        
        _logger.LogInformation(
            "HTTP {method} {path} Initiating request.  | RequestID={requestId} | UserId={userId}",
            method,
            path,
            requestId,
            userId);
        
        await _next(context);
        stopwatch.Stop();
        
        _logger.LogInformation(
            "HTTP {method} {path} responded {statusCode} in {duration} milliseconds | RequestID={requestId} | UserId={userId}",
            method,
            path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            requestId,
            userId);
    }

    private String GetOrCreateCorrelationId(HttpContext context)
    {
        if(context.Request.Headers.TryGetValue(CorrelationHeaderName, out var correlationId))
            return correlationId;
        
        return Guid.NewGuid().ToString();
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}